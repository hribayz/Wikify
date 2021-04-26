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

        public async Task<ILicense> GetLicenseAsync(IImageIdentifier identifier)
        {
            // Compose query.
            var licenseQuery = MediaWikiUtils.GetImageMetadataQuery(new[] { identifier.Title });
            var licenseQueryUri = new Uri(licenseQuery);

            _logger.LogInformation("Parse Query: " + licenseQuery);

            // Retrieve image metadata.
            var responseContent = await _networkingProvider.GetResponseContentAsync(licenseQueryUri);

            _logger.LogInformation("Parse Query response content: " + responseContent);

            var imageInfoResponse = JsonConvert.DeserializeObject<ImageInfoResponse>(responseContent);

            // Check response validity.
            var imagePage = imageInfoResponse?.query?.pages?.SingleOrDefault();
            var imageInfo = imagePage?.Value?.imageinfo?.SingleOrDefault();

            if (imageInfo?.extmetadata == null)
            {
                var errorMessage = "Failed to retrieve image info. Was there something missing in the response?";
                _logger.LogError(errorMessage);
                throw new ApplicationException(errorMessage);
            }

            // Create copyright license enum.
            var allExtMetadataAttributes = imageInfo.extmetadata.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.value));
            var copyrightLicense = _copyrightTokenizer.GetCopyrightLicense(allExtMetadataAttributes);

            IAttribution attribution;

            // Try fetch artist name.
            var artistName = imageInfo.extmetadata.GetValueOrDefault("Artist")?.value;
            if (!string.IsNullOrWhiteSpace(artistName))
            {
                attribution = _copyrightFactory.CreateAttribution(identifier.Title, artistName, identifier.MetadataUri);
            }
            else
            {
                attribution = _copyrightFactory.CreateAttributionWithoutAuthor(identifier.Title, identifier.MetadataUri);
            }

            // Create license restrictions enum.
            var licenseRestrictions = _licenseRestrictionsTokenizer.GetLicenseRestrictions(allExtMetadataAttributes);

            var copyright = _copyrightFactory.CreateCopyright(copyrightLicense);
            var license = _licenseFactory.CreateLicense(copyright, attribution, licenseRestrictions);

            return license;
        }

        public Task<IImmutableDictionary<IIdentifier, ILicense>> GetLicensesAsync(IEnumerable<IImageIdentifier> identifiers)
        {
            throw new NotImplementedException();
        }


    }
}
