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

namespace Wikify.License
{
    public class ImageLicenseProvider : IImageLicenseProvider
    {
        private ILogger _logger;
        private INetworkingProvider _networkingProvider;
        private ICopyrightFactory _copyrightFactory;
        private ILicenseFactory _licenseFactory;

        public ImageLicenseProvider(ILogger logger, INetworkingProvider networkingProvider, ICopyrightFactory copyrightFactory, ILicenseFactory licenseFactory)
        {
            _logger = logger;
            _networkingProvider = networkingProvider;
            _copyrightFactory = copyrightFactory;
            _licenseFactory = licenseFactory;
        }

        public async Task<ILicense> GetLicenseAsync(IImageIdentifier identifier)
        {
            // Compose query.
            var licenseQuery = MediaWikiUtils.GetImageMetadataQuery(new[] { identifier.Title });
            var licenseQueryUri = new Uri(licenseQuery);

            var logSb = new StringBuilder().Append("Parse Query: ").Append(licenseQuery).Append(Environment.NewLine);

            // Retrieve image metadata.
            var responseContent = await _networkingProvider.GetResponseContentAsync(licenseQueryUri);

            logSb.Append("Parse Query response content: ").Append(responseContent);

            var imageInfoResponse = JsonConvert.DeserializeObject<ImageInfoResponse>(responseContent);

            // Check response validity.
            var imagePage = imageInfoResponse?.query?.pages?.SingleOrDefault();
            var imageInfo = imagePage?.Value?.imageinfo?.SingleOrDefault();

            if (imageInfo?.extmetadata == null)
            {
                logSb.Append("Failed to retrieve image info. Was there something missing in the response?").Append(Environment.NewLine);
                throw new ApplicationException(logSb.ToString());
            }

            // TODO: if no credit retrieved (or maybe always, the entries are low effort), use wikipedia file page instead. If no object name retrieved, use filename instead.

            var keysToCheck = new[] { "LicenseShortName", "ObjectName", "Artist" };

            bool allKeysPresent = true;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.

            foreach (var key in keysToCheck)
            {
                bool isLicenseShortNamePresent = imageInfo.extmetadata.ContainsKey(key) && imageInfo.extmetadata[key].value != null;

                if (!isLicenseShortNamePresent)
                {
                    logSb.Append("Failed to retrieve license name from imageInfo.extmetadata. Missing key: ").Append(key).Append(Environment.NewLine);
                    allKeysPresent = false;
                }
            }

            if (!allKeysPresent)
            {
                throw new ApplicationException(logSb.ToString());
            }

            var copyrightLicense = _copyrightFactory.ParseLicense(imageInfo.extmetadata["LicenseShortName"].value);
            var attribution = _copyrightFactory.CreateAttribution(imageInfo.extmetadata["ObjectName"].value, imageInfo.extmetadata["Artist"].value, identifier.MetadataUri);

#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var copyright = _copyrightFactory.CreateCopyright(copyrightLicense);

            var license = _licenseFactory.CreateLicense(copyright, attribution,);



            // TODO: Just for debug purposes, check here that if the new license object has IsAttributionRequired == false, the MW Api says the same.

            Debug.Assert()

            imageInfoResponse.query
        }

        public Task<IImmutableDictionary<IIdentifier, ILicense>> GetLicensesAsync(IEnumerable<IImageIdentifier> identifiers)
        {
            throw new NotImplementedException();
        }

        private LicenseRestrictionsEnum ParseRestrictions(string categories, string imageDescription, string restrictions)
        {

        }

    }
}
