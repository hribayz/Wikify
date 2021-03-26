using System;
using Wikify.Common;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.License
{
    public interface ILicenseProvider
    {
        public ILicense GetLicense(IIdentifier wikiObjectIdentifier);
    }
    public class LicenseProvider : ILicenseProvider
    {
        public ILicense GetLicense(IIdentifier wikiObjectIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}
