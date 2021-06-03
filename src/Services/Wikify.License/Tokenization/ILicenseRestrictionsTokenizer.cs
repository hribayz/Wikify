using System.Collections.Generic;
using Wikify.Common.License;

namespace Wikify.License.Tokenization
{
    public interface ILicenseRestrictionsTokenizer
    {
        public LicenseRestrictionsEnum GetLicenseRestrictions(IEnumerable<KeyValuePair<string, string>> attributes);
    }
}
