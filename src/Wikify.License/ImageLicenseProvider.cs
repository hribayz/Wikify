﻿using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.License;
using Wikify.Common.MediaWikiModels;
using Wikify.Common.Network;
using Wikify.License.Copyright;
using Wikify.License.Tokenization;
using static Wikify.Common.MediaWikiModels.MediaWikiImageInfoResponse;

namespace Wikify.License
{
    /// <summary>
    /// Licensing resolving service.
    /// </summary>
    public class ImageLicenseProvider : IImageLicenseProvider
    {
        private ILogger _logger;
        private INetworkingProvider _networkingProvider;
        private ICopyrightFactory _copyrightFactory;
        private ILicenseFactory _licenseFactory;
        private ICopyrightTokenizer _copyrightTokenizer;
        private ILicenseRestrictionsTokenizer _licenseRestrictionsTokenizer;

        public ImageLicenseProvider(
            ILogger logger,
            INetworkingProvider networkingProvider,
            ICopyrightFactory copyrightFactory,
            ILicenseFactory licenseFactory,
            ICopyrightTokenizer tokenizer,
            ILicenseRestrictionsTokenizer licenseRestrictionsTokenizer)
        {
            _logger = logger;
            _networkingProvider = networkingProvider;
            _copyrightFactory = copyrightFactory;
            _licenseFactory = licenseFactory;
            _copyrightTokenizer = tokenizer;
            _licenseRestrictionsTokenizer = licenseRestrictionsTokenizer;
        }

        private async Task<ImageInfoRootObject?> QueryLicensesAsync(IEnumerable<IImageIdentifier> imageIdentifiers)
        {
            // Compose request

            string licenseQuery = MediaWikiUtils.GetImageMetadataQuery(imageIdentifiers.Select(x => x.Title), new[] { "extmetadata" });
            var licenseQueryUri = new Uri(licenseQuery);

            _logger.LogInformation("Parse Query: " + licenseQuery);

            // Query the API.

            var responseContent = await _networkingProvider.GetResponseContentAsync(licenseQueryUri);

            _logger.LogInformation("Parse Query response content: " + responseContent);

            return JsonConvert.DeserializeObject<ImageInfoRootObject>(responseContent);
        }

        private async Task<ILicense> TokenizeLicenseMetadataWithValidationAsync(IImageIdentifier identifier, IEnumerable<KeyValuePair<string, Metadata>>? metaAttributes)
        {
            if (metaAttributes is null)
            {
                _logger.LogError(nameof(TokenizeLicenseMetadataWithValidationAsync) + " Cannot tokenize metadata, null " + nameof(metaAttributes));
                throw new ArgumentNullException(nameof(metaAttributes));
            }

            // Metadata dictionary is valid here. Attributes have non-empty keys and non-null values.

            var attributesDictionary = metaAttributes.ToDictionary(x => x.Key, x => x.Value.value);

            return await TokenizeLicenseMetadata(identifier, attributesDictionary);
        }

        private async Task<ILicense> TokenizeLicenseMetadata(IImageIdentifier identifier, IReadOnlyDictionary<string, string> attributes)
        {

            // Tokenize license on a background thread.

            var copyrightLicenseTask =
                Task.Run(() => _copyrightTokenizer.GetCopyrightLicense(attributes));

            var artistName = attributes.GetValueOrDefault("Artist");

            IAttribution attribution = string.IsNullOrWhiteSpace(artistName)
                ? _copyrightFactory.CreateAttributionWithoutAuthor(identifier.Title, identifier.CreditUri)
                : _copyrightFactory.CreateAttribution(identifier.Title, artistName, identifier.CreditUri);


            var licenseRestrictions = _licenseRestrictionsTokenizer.GetLicenseRestrictions(attributes);

            var copyright = _copyrightFactory.CreateCopyright(await copyrightLicenseTask);

            // Retrieve tokenized license from background job.

            var license = _licenseFactory.CreateLicense(copyright, attribution, licenseRestrictions);

            return license;
        }

        /// <summary>
        /// Provides licensing information for an image.
        /// </summary>
        /// <param name="identifier">Id of the image to get license for.</param>
        /// <returns>Licensing information.</returns>
        public async Task<ILicense> GetImageLicenseAsync(IImageIdentifier identifier)
        {
            var imageInfoResponse = await QueryLicensesAsync(new[] { identifier });

            Dictionary<string, MediaWikiImageInfoResponse.Metadata>? metaAttributes =
                imageInfoResponse?.query?.pages?.SingleOrDefault().Value?.imageinfo?.SingleOrDefault()?.extmetadata;

            return await TokenizeLicenseMetadataWithValidationAsync(identifier, metaAttributes);
        }

        /// <summary>
        /// Provides licensing information for a collection of images.
        /// </summary>
        /// <param name="identifiers">Ids of the images to get licenses for.</param>
        /// <returns>Licensing information.</returns>
        public async Task<IReadOnlyDictionary<IImageIdentifier, ILicense>> GetImageLicensesAsync(IEnumerable<IImageIdentifier> identifiers)
        {
            var imageInfo = await QueryLicensesAsync(identifiers);

            // Validate response.

            if (imageInfo?.query?.pages == null)
            {
                throw new ApplicationException("Failed to retrieve image info. imageInfosResponse?.query?.pages is null.");
            }

            var originalTitles = new Dictionary<string, string>();

            if (imageInfo.query.normalized != null)
            {
                originalTitles = imageInfo.query.normalized.ToDictionary(x => x.to, x => x.from);
            }

            List<Task<KeyValuePair<IImageIdentifier, ILicense>>> licenseTokenizationTasks = new();

            foreach (var page in imageInfo.query.pages)
            {
                var metaAttributes = page.Value?.imageinfo?.SingleOrDefault()?.extmetadata;

                if (metaAttributes == null)
                {
                    _logger.LogError(nameof(GetImageLicensesAsync) + " cannot retrieve object title, null " + nameof(metaAttributes));
                    throw new ArgumentNullException(nameof(metaAttributes));
                }

                // Response is valid here. The metadata object is present. Attributes have non-empty keys and non-null values.

                var normalizedTitle = metaAttributes["ObjectName"].value;

                var matchingIdentifier = identifiers.Single(x => (x.Title == normalizedTitle || x.Title == originalTitles[normalizedTitle]));

                licenseTokenizationTasks.Add(Task.Run(async () =>
                {
                    return new KeyValuePair<IImageIdentifier, ILicense>(
                        matchingIdentifier,
                        await TokenizeLicenseMetadataWithValidationAsync(matchingIdentifier, metaAttributes));
                }));
            }

            var licenseKeyValuePairs = await Task.WhenAll(licenseTokenizationTasks);

            return new Dictionary<IImageIdentifier, ILicense>(licenseKeyValuePairs);
        }


        public async Task<ILicense> GetImageLicenseAsync(IImageIdentifierWithMetadata imageIdentifier)
        {
            return await TokenizeLicenseMetadata(imageIdentifier, imageIdentifier.ImageMetadata);
        }

        public async Task<IReadOnlyDictionary<IImageIdentifier, ILicense>> GetImageLicensesAsync(IEnumerable<IImageIdentifierWithMetadata> imageIdentifiers)
        {
            List<Task<KeyValuePair<IImageIdentifier, ILicense>>> licenseTokenizationTasks = new();

            foreach (var identifier in imageIdentifiers)
            {
                licenseTokenizationTasks.Add(Task.Run(async () =>
                {
                    return new KeyValuePair<IImageIdentifier, ILicense>(
                        identifier,
                        await TokenizeLicenseMetadata(identifier, identifier.ImageMetadata));
                }));
            }

            var licenseKeyValuePairs = await Task.WhenAll(licenseTokenizationTasks);

            return new Dictionary<IImageIdentifier, ILicense>(licenseKeyValuePairs);
        }
    }
}
