using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.License;
using Wikify.Common.MediaWikiModels;
using Wikify.Common.Network;
using Wikify.License.Copyright;
using Wikify.License.Tokenization;

namespace Wikify.License
{
    public class LicenseProvider : ILicenseProvider
    {
        private ILogger _logger;
        private INetworkingProvider _networkingProvider;
        private ICopyrightFactory _copyrightFactory;
        private ILicenseFactory _licenseFactory;
        private ICopyrightTokenizer _copyrightTokenizer;
        private ILicenseRestrictionsTokenizer _licenseRestrictionsTokenizer;

        public LicenseProvider(
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

        private async Task<ImageInfoResponse?> QueryLicensesAsync(IEnumerable<IImageIdentifier> imageIdentifiers)
        {
            // Compose request

            string licenseQuery = MediaWikiUtils.GetImageMetadataQuery(imageIdentifiers.Select(x => x.Title));
            var licenseQueryUri = new Uri(licenseQuery);

            _logger.LogInformation("Parse Query: " + licenseQuery);

            // Query the API.

            var responseContent = await _networkingProvider.GetResponseContentAsync(licenseQueryUri);

            _logger.LogInformation("Parse Query response content: " + responseContent);

            return JsonConvert.DeserializeObject<ImageInfoResponse>(responseContent);
        }

        private async Task<ILicense> TokenizeLicenseMetadataWithValidationAsync(IImageIdentifier identifier, Dictionary<string, Metadata>? metaAttributes)
        {
            if (metaAttributes is null)
            {
                _logger.LogError(nameof(TokenizeLicenseMetadataWithValidationAsync) + " Cannot tokenize metadata, null " + nameof(metaAttributes));
                throw new ArgumentNullException(nameof(metaAttributes));
            }

            // Valid metadata dictionary. Attributes have non-empty keys and non-null values.

            var attributesKeyValuePairs =
                metaAttributes.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.value));

            // Tokenize license on a background thread.

            var copyrightLicenseTask =
                Task.Run(() => _copyrightTokenizer.GetCopyrightLicense(attributesKeyValuePairs));

            var artistName = metaAttributes.GetValueOrDefault("Artist")?.value;

            IAttribution attribution = string.IsNullOrWhiteSpace(artistName)
                ? _copyrightFactory.CreateAttributionWithoutAuthor(identifier.Title, identifier.MetadataUri)
                : _copyrightFactory.CreateAttribution(identifier.Title, artistName, identifier.MetadataUri);


            var licenseRestrictions = _licenseRestrictionsTokenizer.GetLicenseRestrictions(attributesKeyValuePairs);

            var copyright = _copyrightFactory.CreateCopyright(await copyrightLicenseTask);

            // Retrieve tokenized license from background job.

            var license = _licenseFactory.CreateLicense(copyright, attribution, licenseRestrictions);

            return license;
        }

        public async Task<ILicense> GetImageLicenseAsync(IImageIdentifier identifier)
        {
            var imageInfoResponse = await QueryLicensesAsync(new[] { identifier });

            Dictionary<string, Metadata>? metaAttributes =
                imageInfoResponse?.query?.pages?.SingleOrDefault().Value?.imageinfo?.SingleOrDefault()?.extmetadata;

            return await TokenizeLicenseMetadataWithValidationAsync(identifier, metaAttributes);
        }

        public async Task<IImmutableDictionary<IImageIdentifier, ILicense>> GetImageLicensesAsync(IEnumerable<IImageIdentifier> identifiers)
        {
            var imageInfosResponse = await QueryLicensesAsync(identifiers);

            // Validate response.

            if (imageInfosResponse?.query?.pages == null)
            {
                throw new ApplicationException("Failed to retrieve image info. imageInfosResponse?.query?.pages is null.");
            }

            var originalTitles = new Dictionary<string, string>();

            if (imageInfosResponse.query.normalized != null)
            {
                originalTitles = imageInfosResponse.query.normalized.ToDictionary(x => x.to, x => x.from);
            }

            List<Task<KeyValuePair<IImageIdentifier, ILicense>>> licenseTokenizationTasks = new();

            foreach (var page in imageInfosResponse.query.pages)
            {
                var metaAttributes = page.Value?.imageinfo?.SingleOrDefault()?.extmetadata;

                // Response is valid here. The metadata object is present. Attributes have non-empty keys and non-null values.

                if (metaAttributes == null)
                {
                    _logger.LogError(nameof(GetImageLicensesAsync) + " cannot retrieve object title, null " + nameof(metaAttributes));
                    throw new ArgumentNullException(nameof(metaAttributes));
                }

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

            return licenseKeyValuePairs.ToImmutableDictionary();
        }

        public Task<ILicense> GetArticleLicenseAsync(IArticleIdentifier articleIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}
