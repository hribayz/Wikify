using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.License;

namespace Wikify.License.Copyright
{
    public interface ICopyrightFactory
    {
        public ICopyright CreateCopyright(CopyrightLicenseEnum copyrightLicense);
        public CopyrightLicenseEnum ParseLicense(string license);
        public IAttribution CreateAttribution(string title, string author, string credit);
    }
}
