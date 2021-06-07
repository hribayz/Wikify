using Microsoft.Extensions.Logging;
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
using Wikify.Common.Network;
using Wikify.License.Copyright;
using Wikify.License.Tokenization;
using static Wikify.Common.Domain.Models.MediaWiki.ImageInfoResponse;

namespace Wikify.License
{
    /// <summary>
    /// Licensing resolving service.
    /// </summary>
    public class ImageLicenseProvider : IImageLicenseProvider
    {
        private ILogger _logger;
        private ICopyrightFactory _copyrightFactory;
        private ILicenseFactory _licenseFactory;
        private ICopyrightTokenizer _copyrightTokenizer;
        private ILicenseRestrictionsTokenizer _licenseRestrictionsTokenizer;

        public ImageLicenseProvider(
            ILogger logger,
            ICopyrightFactory copyrightFactory,
            ILicenseFactory licenseFactory,
            ICopyrightTokenizer tokenizer,
            ILicenseRestrictionsTokenizer licenseRestrictionsTokenizer)
        {
            _logger = logger;
            _copyrightFactory = copyrightFactory;
            _licenseFactory = licenseFactory;
            _copyrightTokenizer = tokenizer;
            _licenseRestrictionsTokenizer = licenseRestrictionsTokenizer;
        }


        /// <summary>
        /// Provides licensing information for a collection of images.
        /// </summary>
        /// <param name="identifiers">Ids of the images to get licenses for.</param>
        /// <returns>Licensing information.</returns>
        public async Task<IReadOnlyDictionary<IImageIdentifier, ILicense>> GetImageLicensesAsync(IEnumerable<IImageIdentifier> identifiers)
        {
            List<Task<KeyValuePair<IImageIdentifier, ILicense>>> licenseTokenizationTasks = new();

            foreach (var identifier in identifiers)
            {
                licenseTokenizationTasks.Add(Task.Run(async () =>
                {
                    return new KeyValuePair<IImageIdentifier, ILicense>(
                        identifier,
                        await GetImageLicenseAsync(identifier));
                }));
            }

            var licenseKeyValuePairs = await Task.WhenAll(licenseTokenizationTasks);

            return new Dictionary<IImageIdentifier, ILicense>(licenseKeyValuePairs);
        }


        public async Task<ILicense> GetImageLicenseAsync(IImageIdentifier identifier)
        {
            // Tokenize license on a background thread.
            var copyrightLicenseTask =
                Task.Run(() => _copyrightTokenizer.GetCopyrightLicense(identifier.ImageMetadata));

            // Not all images have the Artist attribute. 
            var artistName = identifier.ImageMetadata.GetValueOrDefault("Artist");

            IAttribution attribution = string.IsNullOrWhiteSpace(artistName)
                ? _copyrightFactory.CreateAttributionWithoutAuthor(identifier.Title, identifier.CreditUri)
                : _copyrightFactory.CreateAttribution(identifier.Title, artistName, identifier.CreditUri);


            var licenseRestrictions = _licenseRestrictionsTokenizer.GetLicenseRestrictions(identifier.ImageMetadata);

            var copyright = _copyrightFactory.CreateCopyright(await copyrightLicenseTask);

            // Retrieve tokenized license from background job.
            var license = _licenseFactory.CreateLicense(copyright, attribution, licenseRestrictions);

            return license;
        }
    }
}
