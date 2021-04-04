using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.Network;
using Wikify.License;

namespace Wikify.Archive.AngleSharp
{
    public class Downloader : IArchive<IWikiArticle>, IArchive<IWikiImage>
    {
        private readonly ILicenseProvider _licenseProvider;
        private readonly ILogger _logger;
        private readonly Action _networkFailed;
        private HttpClient _client;

        public Downloader(ILogger logger, INetworkingProvider networkingProvider, ILicenseProvider licenseProvider)
        {
            _licenseProvider = licenseProvider;
            _logger = logger;
            _client = networkingProvider.GetHttpClient();
            _networkFailed = () =>
            {
                _logger.LogError("network failed");
                _client = networkingProvider.GetHttpClient();
            };
        }

        public async Task<IWikiArticle> GetMediaAsync(IIdentifier<IWikiArticle> elementIdentifier)
        {
            try
            {
                throw new NotImplementedException();
            }

            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<IWikiImage> GetMediaAsync(IIdentifier<IWikiImage> elementIdentifier)
        {
            try
            {
                throw new NotImplementedException();
            }

            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
    }
}
