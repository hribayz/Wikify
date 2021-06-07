using System.Collections.Generic;
using Wikify.Common.Domain;

namespace Wikify.License.Tokenization
{
    public interface ILicenseRestrictionsTokenizer
    {
        public LicenseRestrictionsEnum GetLicenseRestrictions(IEnumerable<KeyValuePair<string, string>> attributes);
    }
}
