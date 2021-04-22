using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.License;

namespace Wikify.License
{
    internal class License : ILicense
    {
        public CopyrightLicense Copyright { get; }
        public LicenseRestrictionsEnum Restrictions { get; }

        public License(CopyrightLicense copyrightLicense, LicenseRestrictionsEnum licenseRestrictions)
        {
            Copyright = copyrightLicense;
            Restrictions = licenseRestrictions;
        }
    }
    public class LicenseFactory : ILicenseFactory
    {
        public ILicense CreateLicense(CopyrightLicense copyrightLicense, LicenseRestrictionsEnum licenseRestrictions)
        {
            return new License(copyrightLicense, licenseRestrictions);
        }
    }
}
