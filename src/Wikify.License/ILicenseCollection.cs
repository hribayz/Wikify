using System.Collections.Generic;
using System.Collections.Immutable;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.License
{
    public interface ILicenseCollection
    {
        public IImmutableDictionary<IIdentifier, ILicense> All { get; }
        public IEnumerable<KeyValuePair<IIdentifier, ILicense>> GetCompatibleWith(LicenseType licenseType);
    }
}
