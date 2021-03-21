using System;
using Wikify.Common;

namespace Wikify.License
{
    public interface ILicenseProvider
    {
        public AWikiObjectLicense GetLicense(AWikiObjectIdentifier wikiObjectIdentifier);
    }
}
