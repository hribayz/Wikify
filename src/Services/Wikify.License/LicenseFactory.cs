using Wikify.Common.License;

namespace Wikify.License
{
    public class LicenseFactory : ILicenseFactory
    {
        public ILicense CreateLicense(ICopyright copyright, IAttribution attribution, LicenseRestrictionsEnum licenseRestrictions)
        {
            return new License(copyright, attribution, licenseRestrictions);
        }
    }
}
