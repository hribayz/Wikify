using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.Network;
using Wikify.License;

namespace Wikify.Archive
{
    public class ArticleDownloader : DownloaderBase, IArticleArchive
    {
        public ArticleDownloader(ILogger logger, INetworkingProvider networkingProvider, ILicenseProvider licenseProvider, IWikiMediaFactory wikiMediaFactory) :
            base(logger, networkingProvider, licenseProvider, wikiMediaFactory)
        {

        }
        public async Task<IWikiArticle> GetArticleAsync(IArticleIdentifier articleIdentifier, WikiContentModel contentModel)
        {
            try
            {
                var articleHtmlTask = _client.GetStringAsync(articleIdentifier.GetEndpoint(contentModel));
                var license = await _licenseProvider.GetLicenseAsync(articleIdentifier);
                var articleHtml = await articleHtmlTask;

                return _wikiMediaFactory.CreateWikiArticle(articleIdentifier, license, articleHtml, contentModel);
            }
            // TODO : try to recover from networking related exceptions by re-instantiating client
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
    }
}
