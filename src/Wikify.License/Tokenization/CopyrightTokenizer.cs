using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Wikify.Common.License;

namespace Wikify.License.Tokenization
{
    public class CopyrightTokenizer : ICopyrightTokenizer
    {

        private IMediaWikiConstantsContainer _constants;
        public CopyrightTokenizer(IMediaWikiConstantsContainer mediaWikiConstantsContainer)
        {
            _constants = mediaWikiConstantsContainer;
        }

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
            return CopyrightLicenseEnum.Unknown;
        }
    }
}
