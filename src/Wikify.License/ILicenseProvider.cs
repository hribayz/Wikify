using System;
using Wikify.Common;
using Wikify.Common.License;

namespace Wikify.License
{
    public interface ILicenseProvider
    {
        public IWikiObjectLicense GetLicense(AWikiObjectIdentifier wikiObjectIdentifier);
    }
    public class LicenseProvider : ILicenseProvider
    {
        public IWikiObjectLicense GetLicense(AWikiObjectIdentifier wikiObjectIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}
