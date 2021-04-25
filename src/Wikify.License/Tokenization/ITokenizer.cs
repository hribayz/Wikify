using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.License;

namespace Wikify.License.Tokenization
{
    public interface ITokenizer
    {
        public LicenseRestrictionsEnum GetLicenseRestrictions(IEnumerable<KeyValuePair<string, string>> attributes);
        public CopyrightLicenseEnum GetCopyrightLicense(IEnumerable<KeyValuePair<string, string>> attributes);
    }
}
