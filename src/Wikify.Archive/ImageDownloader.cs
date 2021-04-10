using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.Network;
using Wikify.License;

namespace Wikify.Archive.AngleSharp
{
    public class ImageDownloader : DownloaderBase, IArchive<IWikiImage>
    {
        public ImageDownloader(ILogger logger, INetworkingProvider networkingProvider, ILicenseProvider licenseProvider, IWikiMediaFactory wikiMediaFactory) :
            base(logger, networkingProvider, licenseProvider, wikiMediaFactory)
        {
        }

        public async Task<IWikiImage> GetMediaAsync(IIdentifier elementIdentifier)
        {
            try
            {
                var licenseTask = _licenseProvider.GetLicenseAsync(elementIdentifier);

                var imageStream = await _client.GetStreamAsync(elementIdentifier.Uri);
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
