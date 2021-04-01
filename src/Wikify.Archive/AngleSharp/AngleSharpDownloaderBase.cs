using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using Wikify.Common.Network;

namespace Wikify.Archive.AngleSharp
{
    public abstract class AngleSharpDownloaderBase
    {
        protected ILogger _logger;
        protected HttpClient _httpClient;
        protected Action _renewClient;

        public AngleSharpDownloaderBase(ILogger logger, INetworkingProvider networkingProvider)
        {
            _logger = logger;
            _httpClient = networkingProvider.GetHttpClient();
            _renewClient = () => networkingProvider.GetHttpClient();
        }
    }
}
