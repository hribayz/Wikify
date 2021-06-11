using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wikify.Common.Domain.Models.MediaWiki;
using Wikify.Common.Network;
using static Wikify.Common.Domain.Models.MediaWiki.ImageInfoResponse;

namespace Wikify.Common.Id
{
    /// <summary>
    /// Gathers and validates image metadata for the purpose of image identification. 
    /// </summary>
    public class ImageIdProvider : IImageIdProvider
    {
        private IImageIdentifierFactory _imageIdentifierFactory;
        private ILogger _logger;
        private INetworkingProvider _networkingProvider;

        public ImageIdProvider(ILogger<ImageIdProvider> logger, IImageIdentifierFactory imageIdentifierFactory, INetworkingProvider networkingProvider)
        {
            _imageIdentifierFactory = imageIdentifierFactory;
            _logger = logger;
            _networkingProvider = networkingProvider;
        }

        /// <summary>
        /// Provides an identifier object for an image.
        /// </summary>
        /// <param name="imageTitle">File name of the image.</param>
        /// <returns>Image identification object.</returns>
        public async Task<IImageIdentifier> GetIdentifierAsync(string imageTitle)
        {
            var identifier = await GetIdentifiersAsync(new[] { imageTitle });
            return identifier.Single().Value;
        }

        public async Task<IReadOnlyDictionary<string, IImageIdentifier>> GetIdentifiersAsync(IEnumerable<string> imageTitles)
        {
            var props = ImageInfoProps.ExtMetadata | ImageInfoProps.Url;
            var imageQueryResult = await QueryImageMetadataAsync(imageTitles, props) ??
                throw new ApplicationException("Null image metadata object");

            // Validate response.
            if (!MediaWikiUtils.AssertImageInfoPropsNotNull(imageQueryResult, props))
            {
                var errorMessage = nameof(GetIdentifiersAsync) + " cannot retrieve object image info, invalid image query result.";
                _logger.LogError(errorMessage);
                throw new ApplicationException(errorMessage);
            }

            // Response is valid here. The metadata object is present. Attributes have non-empty keys and non-null values.

            var originalTitles = new Dictionary<string, string>();


            if (imageQueryResult.query.normalized != null)
            {
                originalTitles = imageQueryResult.query.normalized.ToDictionary(x => x.to, x => x.from);
            }


            Dictionary<string, IImageIdentifier> imageIdentifiers = new();

            foreach (var page in imageQueryResult.query.pages)
            {
                var imageInfo = page.Value?.imageinfo?.SingleOrDefault() ?? throw new ApplicationException("Null image info object");

                // Null safety of the following dereferences has been asserted by AssertImageInfoPropsNotNull
                var metaAttributes = imageInfo.extmetadata;
                var url = imageInfo.url;
                var descriptionUrl = imageInfo.descriptionurl;
                var normalizedTitle = page.Value.title;

                var matchingTitle = imageTitles.Single(x => (x == normalizedTitle || x == originalTitles[normalizedTitle]));

                var attributesDictionary = metaAttributes.ToDictionary(x => x.Key, x => x.Value.value);

                var imageIdentifier = _imageIdentifierFactory.CreateIdentifier(matchingTitle, descriptionUrl, url, attributesDictionary);

                imageIdentifiers.Add(matchingTitle, imageIdentifier);
            }

            return imageIdentifiers;
        }
        private async Task<ImageInfoRootObject?> QueryImageMetadataAsync(IEnumerable<string> imageTitles, ImageInfoProps iiProps)
        {
            // Compose request

            string metadataQuery = MediaWikiUtils.GetImageMetadataQuery(imageTitles, iiProps);
            var metadataQueryUri = new Uri(metadataQuery);

            _logger.LogInformation("Parse Query: " + metadataQuery);

            // Query the API.

            var responseContent = await _networkingProvider.DownloadContentAsync(metadataQueryUri);

            _logger.LogInformation("Parse Query response content: " + responseContent);

            return JsonConvert.DeserializeObject<ImageInfoRootObject>(responseContent);
        }
    }
}