using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.License;

namespace Wikify.License.Tokenization
{
    /// <summary>
    /// Provides copyright license classification services
    /// </summary>
    public interface ICopyrightTokenizer
    {
        /// <summary>
        /// Provides copyright license classification based on non structured information.
        /// </summary>
        /// <param name="attributes">Non structured copyright licensing information.</param>
        /// <returns></returns>
        public CopyrightLicenseEnum GetCopyrightLicense(IEnumerable<KeyValuePair<string, string>> attributes);
    }
}
