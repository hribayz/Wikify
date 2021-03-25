using System;
using Wikify.Common;
using Wikify.Common.License;

namespace Wikify.License
{
    public interface ILicenseProvider
    {
        public ILicense GetLicense(IObjectIdentifier wikiObjectIdentifier);
    }
    public class LicenseProvider : ILicenseProvider
    {
        public ILicense GetLicense(IObjectIdentifier wikiObjectIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}
