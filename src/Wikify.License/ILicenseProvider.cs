using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Wikify.Common;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.License
{
    public interface ILicenseProvider
    {
        public Task<ILicense> GetLicenseAsync(IIdentifier identifier);
        public Task<ILicenseCollection> GetLicensesAsync(IEnumerable<IIdentifier> identifiers);
    }
}
