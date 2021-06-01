using Humanizer;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Wikify.Common.License;

namespace Wikify.License.Tokenization
{
    /// <summary>
    /// Provides structured copyright information.
    /// </summary>
    public class CopyrightTokenizer : ICopyrightTokenizer
    {
        private ILogger _logger;
        private IMediaWikiConstantsContainer _constants;

        public CopyrightTokenizer(ILogger<CopyrightTokenizer> logger, IMediaWikiConstantsContainer mediaWikiConstantsContainer)
        {
            _logger = logger;
            _constants = mediaWikiConstantsContainer;
        }
        /// <summary>
        /// Extracts copyright insight from unstructured image metadata.
        /// </summary>
        /// <param name="attributes">Key-value pairs of image metadata attributes.</param>
        /// <returns>Image copyright.</returns>
        public CopyrightLicenseEnum GetCopyrightLicense(IEnumerable<KeyValuePair<string, string>> attributes)
        {
            foreach (var attribute in attributes)
            {
                var isLicensingAttribute = _constants.CopyrightKeys.Contains(attribute.Key);

                if (!isLicensingAttribute)
                {
                    continue;
                }

                var normalizedValue = attribute.Value.ToLower().Replace("_", " ").Replace("-", " ");

                if (_constants.PublicDomainEquivalentNames.Contains(normalizedValue))
                {
                    return CopyrightLicenseEnum.PublicDomainEquivalent;
                }

                // disregard anything that comes after CC BY-SA X.Y, like translations or dual licensing notes
                if (Regex.IsMatch(normalizedValue, "cc by sa [0-9]\\.[0-9]"))
                {
                    return CopyrightLicenseEnum.CcBySa;
                }

                // disregard anything that comes after CC BY X.Y, like translations or dual licensing notes
                else if (Regex.IsMatch(normalizedValue, "cc by [0-9]\\.[0-9]"))
                {
                    return CopyrightLicenseEnum.CcBy;
                }

                // disregard anything that comes after GDFL 1.X, like translations or dual licensing notes
                else if (Regex.IsMatch(normalizedValue, "gdfl [0-9]\\.[0-9]"))
                {
                    return CopyrightLicenseEnum.Gfdl;
                }
            }

            // No attribute with known key had a value matching known copyright license pattern.

            _logger.LogWarning(new StringBuilder()
                .Append(nameof(CopyrightTokenizer))
                .Append(" failed to tokenize copyright license. Attributes: ")
                .Append(Environment.NewLine)
                .Append(attributes.Humanize())
                .ToString());
                
            return CopyrightLicenseEnum.Unknown;
        }
    }
}
