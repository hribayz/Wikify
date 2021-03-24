using System;
using System.Net.Http;
using Wikify.Common.Network;

namespace Wikify.Sample.Network
{
    /// <summary>
    /// Mockup implementation that relies on the client code to treat the HttpClient instance nicely
    /// </summary>
    internal class NetworkingProvider : INetworkingProvider
    {
        private HttpClient _httpClient;
        internal NetworkingProvider()
        {
            var handler = new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = new System.Net.CookieContainer(),
                AutomaticDecompression = System.Net.DecompressionMethods.All,
                UseProxy = false
            };

            _httpClient = new HttpClient(handler);
        }

        HttpClient INetworkingProvider.GetHttpClient()
        {
            return _httpClient;
        }
    }
}
