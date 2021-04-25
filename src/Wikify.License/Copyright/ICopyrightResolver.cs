using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.License.Copyright
{
    public interface ICopyrightResolver
    {
        public bool IsCopyrighted(CopyrightLicenseEnum copyrightLicense);
        public bool IsAttributionRequired(CopyrightLicenseEnum copyrightLicense);
        public bool IsCompatible(CopyrightLicenseEnum copyrightLicense, CopyrightLicenseEnum withCopyrightLicense);
        public IEnumerable<CopyrightLicenseEnum> GetCompatibleWith(CopyrightLicenseEnum copyrightLicense);
    }

}
