using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.Network;
using Wikify.License;

namespace Wikify.Archive
{
    public class MediaWikiImageDownloader : IImageArchive
    {
        private readonly ILogger _logger;
        private readonly IWikiMediaFactory _wikiMediaFactory;
        private readonly IImageLicenseProvider _imageLicenseProvider;
        private readonly INetworkingProvider _networkingProvider;

        public MediaWikiImageDownloader(ILogger logger, IWikiMediaFactory wikiMediaFactory, IImageLicenseProvider imageLicenseProvider, INetworkingProvider networkingProvider)
        {
            _logger = logger;
            _wikiMediaFactory = wikiMediaFactory;
            _imageLicenseProvider = imageLicenseProvider;
            _networkingProvider = networkingProvider;
        }

        public async Task<IWikiImage> GetImageAsync(IImageIdentifier imageIdentifier)
        {
            var imageAddress = imageIdentifier.ImageUri;
            var imageAddressUri = new Uri(imageAddress);

            // Download image in the background.
            var imageTask = _networkingProvider.DownloadImageAsync(imageAddressUri);

            // Tokenize license in the meantime.
            var license = await _imageLicenseProvider.GetImageLicenseAsync(imageIdentifier);

            var image = await imageTask;

            return _wikiMediaFactory.CreateWikiImage(imageIdentifier, license, image);
        }

        public async Task<IReadOnlyCollection<IWikiImage>> GetImagesAsync(IEnumerable<IImageIdentifier> imageIdentifiers)
        {
            List<Task<IWikiImage>> imageTasks = new();

            foreach (var imageIdentifier in imageIdentifiers)
            {
                imageTasks.Add(GetImageAsync(imageIdentifier));
            }

            return await Task.WhenAll(imageTasks);
        }
    }
}
