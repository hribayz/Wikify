using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.MediaWikiModels;
using Wikify.Common.Network;
using Wikify.License;
using static Wikify.Common.MediaWikiModels.MediaWikiImageInfoResponse;

namespace Wikify.Archive
{
    public class MediaWikiArticleDownloader : IArticleArchive
    {
        private readonly ILogger _logger;
        private readonly IWikiMediaFactory _wikiMediaFactory;
        private readonly IArticleLicenseProvider _articleLicenseProvider;
        private readonly INetworkingProvider _networkingProvider;

        public MediaWikiArticleDownloader(ILogger<MediaWikiArticleDownloader> logger, INetworkingProvider networkingProvider, IArticleLicenseProvider articleLicenseProvider, IWikiMediaFactory wikiMediaFactory)
        {
            _logger = logger;
            _articleLicenseProvider = articleLicenseProvider;
            _wikiMediaFactory = wikiMediaFactory;
            _networkingProvider = networkingProvider;
        }

        public async Task<IWikiArticle> GetArticleAsync(IArticleIdentifier articleIdentifier, TextContentModel contentModel)
        {
            try
            {
                // Resolve license on a background task, retrieve after content has been parsed.
                var licenseTask = _articleLicenseProvider.GetArticleLicenseAsync(articleIdentifier);

                var parseQuery = MediaWikiUtils.GetParseQuery(articleIdentifier.Title, articleIdentifier.Language, contentModel);
                var parseQueryUri = new Uri(parseQuery);

                var mwResponse = await _networkingProvider.DownloadContentAsync(parseQueryUri);

                var mwResponseObject = JsonConvert.DeserializeObject<MediaWikiParserResponse.ParserRootObject>(mwResponse);

                string? content = contentModel switch
                {
                    TextContentModel.Text => mwResponseObject?.parse?.text?["*"],
                    TextContentModel.WikiText => mwResponseObject?.parse?.wikitext?["*"],
                    _ => throw new NotImplementedException()
                };

                if (content == null)
                {
                    var logSb = new StringBuilder().Append("Failed to deserialize MediaWiki parser output.").Append(Environment.NewLine)
                        .Append("Media Wiki response content: ").Append(mwResponse);

                    throw new ApplicationException(logSb.ToString());
                }

                // retrieve license from background task
                var license = await licenseTask;

                return _wikiMediaFactory.CreateWikiArticle(articleIdentifier, license, content, contentModel);
            }

            catch (Exception e)
            {
                _logger.LogError(e, "Failed to retrieve article.");
                throw;
            }
        }
    }
}
