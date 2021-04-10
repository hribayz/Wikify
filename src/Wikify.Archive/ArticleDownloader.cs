using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.Network;
using Wikify.License;

namespace Wikify.Archive.AngleSharp
{
    public class ArticleDownloader : DownloaderBase, IArchive<IWikiArticle>
    {
        public ArticleDownloader(ILogger logger, INetworkingProvider networkingProvider, ILicenseProvider licenseProvider, IWikiMediaFactory wikiMediaFactory) :
            base(logger, networkingProvider, licenseProvider, wikiMediaFactory)
        {

        }
        public async Task<IWikiArticle> GetMediaAsync(IIdentifier elementIdentifier)
        {
            try
            {
                var articleHtmlTask = _client.GetStringAsync(elementIdentifier.Uri);
                var license = await _licenseProvider.GetLicenseAsync(elementIdentifier);
                var articleHtml = await articleHtmlTask;

                return _wikiMediaFactory.CreateWikiArticle(elementIdentifier, license, articleHtml);
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
