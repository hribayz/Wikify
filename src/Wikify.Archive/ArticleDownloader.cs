using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
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
                var parseQuery = _utils.GetParseQuery(articleIdentifier.Title, articleIdentifier.Language, contentModel);
                var contentTask = _client.GetStringAsync(parseQuery);

                var license = await _licenseProvider.GetLicenseAsync(articleIdentifier);
                var content = await contentTask;

                return _wikiMediaFactory.CreateWikiArticle(articleIdentifier, license, content, contentModel);
            }

            // TODO : Let networking provider know that the client does not work if it's its fault
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
