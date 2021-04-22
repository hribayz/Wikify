using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.License
{
    public interface ILicenseCollection
    {
        public IImmutableDictionary<IIdentifier, ILicense> All { get; }
        public IEnumerable<KeyValuePair<IIdentifier, ILicense>> GetCompatibleWith(CopyrightLicense copyrightLicense);
        public IEnumerable<KeyValuePair<IIdentifier, ILicense>> GetUnrestricted(LicenseRestrictionsEnum disallowedWarnings);
        public IEnumerable<KeyValuePair<IIdentifier, ILicense>> GetCompatibleWithAndUnrestricted(CopyrightLicense copyrightLicense, LicenseRestrictionsEnum disallowedWarnings);
    }
    public class LicenseCollection : ILicenseCollection
    {
        public IImmutableDictionary<IIdentifier, ILicense> All { get; private set; }

        public LicenseCollection(IEnumerable<KeyValuePair<IIdentifier, ILicense>> licenses)
        {
            All = licenses.ToImmutableDictionary();
        }

        public IEnumerable<KeyValuePair<IIdentifier, ILicense>> GetCompatibleWith(CopyrightLicense copyrightLicense)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<KeyValuePair<IIdentifier, ILicense>> GetUnrestricted(LicenseRestrictionsEnum disallowedWarnings)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<KeyValuePair<IIdentifier, ILicense>> GetCompatibleWithAndUnrestricted(CopyrightLicense copyrightLicense, LicenseRestrictionsEnum disallowedWarnings)
        {
            throw new System.NotImplementedException();
        }
    }
}
