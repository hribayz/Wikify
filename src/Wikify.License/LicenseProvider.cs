using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.License
{
    public class LicenseProvider : ILicenseProvider
    {
        public Task<ILicense> GetLicenseAsync(IIdentifier identifier)
        {

            throw new NotImplementedException();
        }
    }
}
