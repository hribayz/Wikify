using System;
using Wikify.Common;
using Wikify.Common.License;

namespace Wikify.License
{
    public interface ILicenseProvider
    {
        public IObjectLicense GetLicense(AObjectIdentifier wikiObjectIdentifier);
    }
    public class LicenseProvider : ILicenseProvider
    {
        public IObjectLicense GetLicense(AObjectIdentifier wikiObjectIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}
