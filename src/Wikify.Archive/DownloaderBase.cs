using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Wikify.Common.Content;
using Wikify.Common.Network;
using Wikify.License;

namespace Wikify.Archive
{
    public abstract class DownloaderBase
    {
        protected readonly ILicenseProvider _licenseProvider;
        protected readonly ILogger _logger;
        protected readonly IWikiMediaFactory _wikiMediaFactory;
        protected readonly Action _networkFailed;
        protected HttpClient _client;

        public DownloaderBase(ILogger logger, INetworkingProvider networkingProvider, ILicenseProvider licenseProvider, IWikiMediaFactory wikiMediaFactory)
        {
            _logger = logger;
            _licenseProvider = licenseProvider;
            _wikiMediaFactory = wikiMediaFactory;

            _client = networkingProvider.GetHttpClient();
            _networkFailed = () =>
            {
                _logger.LogError("network failed");
                _client = networkingProvider.GetHttpClient();
            };
        }
    }
}
