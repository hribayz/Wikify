using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.Network;
using Wikify.License;

namespace Wikify.Archive
{
    public class MediaWikiDownloader : IArticleArchive, IImageArchive
    {
        private readonly MediaWikiUtils _utils;
        private readonly ILicenseProvider _licenseProvider;
        private readonly ILogger _logger;
        private readonly IWikiMediaFactory _wikiMediaFactory;
        private readonly Action _networkFailed;
        private HttpClient _client;

        public MediaWikiDownloader(ILogger logger, INetworkingProvider networkingProvider, ILicenseProvider licenseProvider, IWikiMediaFactory wikiMediaFactory)
        {
            _logger = logger;
            _licenseProvider = licenseProvider;
            _wikiMediaFactory = wikiMediaFactory;

            _client = networkingProvider.GetHttpClient();
            _networkFailed = () =>
            {
                _logger.LogError("network failed");
                _client = networkingProvider.GetHttpClient();
            };
            _utils = new MediaWikiUtils();
        }

        public async Task<IWikiArticle> GetArticleAsync(IArticleIdentifier articleIdentifier, WikiContentModel contentModel)
        {
            try
            {
                // Resolve license on a background task, retrieve after content has been parsed.
                var licenseTask = _licenseProvider.GetLicenseAsync(articleIdentifier);

                var parseQuery = _utils.GetParseQuery(articleIdentifier.Title, articleIdentifier.Language, contentModel);
                var mwResponse = await _client.GetStringAsync(parseQuery);
                var mwResponseObject = JsonConvert.DeserializeObject<MediaWikiResponse>(mwResponse);

                string? content = contentModel switch
                {
                    WikiContentModel.Text => mwResponseObject?.parse?.text?["*"],
                    WikiContentModel.WikiText => mwResponseObject?.parse?.wikitext?["*"],
                    _ => throw new NotImplementedException()
                };

                if (content == null)
                {
                    string errorMessage = new StringBuilder()
                        .Append("Failed to deserialize MediaWiki parser output.").Append(Environment.NewLine)
                        .Append("Parse Query: ").Append(parseQuery).Append(Environment.NewLine)
                        .Append("Media Wiki response: ").Append(mwResponse)
                        .ToString();

                    _logger.LogError(errorMessage);

                    throw new ApplicationException(errorMessage);
                }

                var license = await licenseTask;
                
                return _wikiMediaFactory.CreateWikiArticle(articleIdentifier, license, content, contentModel);
            }

            // TODO : Let the networking provider know that the client does not work if it's its fault
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public Task<IWikiImage> GetImageAsync(IImageIdentifier imageIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}
