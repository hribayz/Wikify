using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Wikify.Common;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.License
{
    public interface IImageLicenseProvider
    {
        public Task<ILicense> GetLicenseAsync(IImageIdentifier identifier);
        public Task<IImmutableDictionary<IIdentifier, ILicense>> GetLicensesAsync(IEnumerable<IImageIdentifier> identifiers);
    }
}
