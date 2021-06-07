using System.Collections.Generic;
using Wikify.Common.Domain;

namespace Wikify.License.Tokenization
{
    public class LicenseRestrictionsTokenizer : ILicenseRestrictionsTokenizer
    {

        public LicenseRestrictionsEnum GetLicenseRestrictions(IEnumerable<KeyValuePair<string, string>> attributes)
        {
            // TODO media wiki attributes to look for : categories, restrictions, templates.

            return
                LicenseRestrictionsEnum.Geographical |
                LicenseRestrictionsEnum.Indigenious |
                LicenseRestrictionsEnum.Personality |
                LicenseRestrictionsEnum.Related |
                LicenseRestrictionsEnum.Trademark;
        }
    }
}
