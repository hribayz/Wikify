using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.License;
using Wikify.Common.Network;

namespace Wikify.License
{
    public class LicenseProvider : ILicenseProvider
    {
        private ILogger _logger;
        private INetworkingProvider _networkingProvider;
        public Task<ILicense> GetLicenseAsync(IIdentifier identifier)
        {

            throw new NotImplementedException();
        }
    }
}
