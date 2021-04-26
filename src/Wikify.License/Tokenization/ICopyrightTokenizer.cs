using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.License;

namespace Wikify.License.Tokenization
{
    public interface ICopyrightTokenizer
    {
        public CopyrightLicenseEnum GetCopyrightLicense(IEnumerable<KeyValuePair<string, string>> attributes);
    }
}
