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
    }
}
