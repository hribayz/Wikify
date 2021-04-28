using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.MediaWikiModels;
using Wikify.Common.Network;
using Wikify.License;

namespace Wikify.Archive
{
    public class MediaWikiDownloader : IArticleArchive, IImageArchive
    {
        private readonly IImageLicenseProvider _licenseProvider;
        private readonly ILogger _logger;
        private readonly IWikiMediaFactory _wikiMediaFactory;
        private readonly INetworkingProvider _networkingProvider;

        public MediaWikiDownloader(ILogger logger, INetworkingProvider networkingProvider, IImageLicenseProvider licenseProvider, IWikiMediaFactory wikiMediaFactory)
        {
            _logger = logger;
            _licenseProvider = licenseProvider;
            _wikiMediaFactory = wikiMediaFactory;
            _networkingProvider = networkingProvider;
        }

        public async Task<IWikiArticle> GetArticleAsync(IArticleIdentifier articleIdentifier, WikiContentModel contentModel)
        {
            try
            {
                // Resolve license on a background task, retrieve after content has been parsed.
                var licenseTask = _licenseProvider.GetArticleLicenseAsync(articleIdentifier);

                var parseQuery = MediaWikiUtils.GetParseQuery(articleIdentifier.Title, articleIdentifier.Language, contentModel);
                var parseQueryUri = new Uri(parseQuery);

                var mwResponse = await _networkingProvider.GetResponseContentAsync(parseQueryUri);

                var mwResponseObject = JsonConvert.DeserializeObject<MediaWikiResponse>(mwResponse);

                string? content = contentModel switch
                {
                    WikiContentModel.Text => mwResponseObject?.parse?.text?["*"],
                    WikiContentModel.WikiText => mwResponseObject?.parse?.wikitext?["*"],
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
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<IWikiImage> GetImageAsync(IImageIdentifier imageIdentifier)
        {
            var licenseTask = _licenseProvider.GetImageLicenseAsync(imageIdentifier);

            imageIdentifier.

        }

        public Task<IReadOnlyCollection<IWikiImage>> GetImagesAsync(IEnumerable<IImageIdentifier> imageIdentifiers)
        {
            throw new NotImplementedException();
        }
    }
}
