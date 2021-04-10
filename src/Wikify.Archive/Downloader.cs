using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.Network;
using Wikify.License;

namespace Wikify.Archive.AngleSharp
{
    public class Downloader : IArchive<IWikiArticle>, IArchive<IWikiImage>
    {
        private readonly ILicenseProvider _licenseProvider;
        private readonly ILogger _logger;
        private readonly IWikiMediaFactory _wikiMediaFactory;
        private readonly Action _networkFailed;
        private HttpClient _client;

        public Downloader(ILogger logger, INetworkingProvider networkingProvider, ILicenseProvider licenseProvider, IWikiMediaFactory wikiMediaFactory)
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
        }

        public async Task<IWikiArticle> GetMediaAsync(IIdentifier<IWikiArticle> elementIdentifier)
        {
            try
            {
                var articleHtmlTask = _client.GetStringAsync(elementIdentifier.Url);
                var license = await _licenseProvider.GetLicenseAsync(elementIdentifier);
                var articleHtml = await articleHtmlTask;

                return _wikiMediaFactory.CreateWikiArticle(elementIdentifier, license, articleHtml);
            }
            // TODO : try to recover from networking related exceptions
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<IWikiImage> GetMediaAsync(IIdentifier<IWikiImage> elementIdentifier)
        {
            try
            {
                var licenseTask = _licenseProvider.GetLicenseAsync(elementIdentifier);
                
                var imageStream = await _client.GetStreamAsync(elementIdentifier.Url);
                var image = Image.FromStream(imageStream);

                var license = await licenseTask;

                return _wikiMediaFactory.CreateWikiImage(elementIdentifier, license, image);
            }
            // TODO : try to recover from networking related exceptions
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
    }
}
