using System;
using Wikify.Common;

namespace Wikify.License
{
    public interface ILicenseProvider
    {
        public AWikiObjectLicense GetLicense(AWikiObjectIdentifier wikiObjectIdentifier);
    }
    public class LicenseProvider : ILicenseProvider
    {
        public AWikiObjectLicense GetLicense(AWikiObjectIdentifier wikiObjectIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}
