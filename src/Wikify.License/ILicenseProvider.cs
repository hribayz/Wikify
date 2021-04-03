using System;
using Wikify.Common;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.License
{
    public interface ILicenseProvider
    {
        public ILicense GetLicense(IWikiMedia media);
    }
}
