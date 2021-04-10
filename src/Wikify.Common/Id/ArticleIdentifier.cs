using System;
using Wikify.Common.Content;

namespace Wikify.Common.Id
{
    public class Identifier : IIdentifier
    {
        public Uri Uri { get; private set; }
        public Uri LicenseUri { get; private set; }
        public Identifier(Uri contentUri, Uri licenseUri)
        {
            Uri = contentUri;
            LicenseUri = licenseUri;
        }
    }
}
