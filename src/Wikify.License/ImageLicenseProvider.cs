using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.License;
using Wikify.Common.MediaWikiModels;
using Wikify.Common.Network;

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
            var imageInfo = JsonConvert.DeserializeObject<ImageInfoResponse>(responseContent);



            var copyrightLicense = _copyrightFactory.CreateCopyright();

            var license = _licenseFactory.CreateLicense();


            // TODO: Just for debug purposes, check here that if the new license object has IsAttributionRequired == false, the MW Api says the same.

            imageInfo.query
        }

        public Task<IImmutableDictionary<IIdentifier, ILicense>> GetLicensesAsync(IEnumerable<IImageIdentifier> identifiers)
        {
            throw new NotImplementedException();
        }
    }
}
