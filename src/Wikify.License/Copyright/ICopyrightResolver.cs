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
    public class CopyrightResolver : ICopyrightResolver
    {
        public IEnumerable<CopyrightLicenseEnum> GetCompatibleWith(CopyrightLicenseEnum copyrightLicense)
        {
            throw new NotImplementedException();
        }

        public bool IsAttributionRequired(CopyrightLicenseEnum copyrightLicense)
        {
            return copyrightLicense switch
            {
                CopyrightLicenseEnum.CcBy => true,
                CopyrightLicenseEnum.CcBySa => true,
                CopyrightLicenseEnum.Gfdl => true,
                CopyrightLicenseEnum.Unknown => true,

                CopyrightLicenseEnum.PublicDomainEquivalent => false,
                _ => throw new NotImplementedException()
            };
        }

        public bool IsCompatible(CopyrightLicenseEnum copyrightLicense, CopyrightLicenseEnum withCopyrightLicense)
        {
            throw new NotImplementedException();
        }

        public bool IsCopyrighted(CopyrightLicenseEnum copyrightLicense)
        {
            return copyrightLicense switch
            {
                CopyrightLicenseEnum.CcBy => true,
                CopyrightLicenseEnum.CcBySa => true,
                CopyrightLicenseEnum.Gfdl => true,
                CopyrightLicenseEnum.Unknown => true,

                CopyrightLicenseEnum.PublicDomainEquivalent => false,
                _ => throw new NotImplementedException()
            };
        }
    }
}
