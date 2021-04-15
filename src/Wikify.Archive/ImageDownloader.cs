using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.Network;
using Wikify.License;

namespace Wikify.Archive
{
    public class ImageDownloader : DownloaderBase, IImageArchive
    {
        public ImageDownloader(ILogger logger, INetworkingProvider networkingProvider, ILicenseProvider licenseProvider, IWikiMediaFactory wikiMediaFactory) :
            base(logger, networkingProvider, licenseProvider, wikiMediaFactory)
        {
        }

        public async Task<IWikiImage> GetImageAsync(IImageIdentifier imageIdentifier)
        {
            try
            {
                var licenseTask = _licenseProvider.GetLicenseAsync(imageIdentifier);

                var imageStream = await _client.GetStreamAsync(imageIdentifier.Endpoint);
                var image = Image.FromStream(imageStream);

                var license = await licenseTask;

                return _wikiMediaFactory.CreateWikiImage(imageIdentifier, license, image);
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
