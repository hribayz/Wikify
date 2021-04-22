using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Common.Network
{
    public class NetworkingProvider : INetworkingProvider
    {
        private HttpClient _httpClient;
        private bool _disposalRunning;
        private ILogger _logger;

        public NetworkingProvider(ILogger logger)
        {
            _logger = logger;
            _disposalRunning = false;

            var handler = new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = new System.Net.CookieContainer(),
                AutomaticDecompression = System.Net.DecompressionMethods.All,
                UseProxy = false
            };

            _httpClient = new HttpClient(handler);
            _httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd(Properties.Resources.UserAgent);
        }

        public async Task<HttpResponseMessage> GetResponseAsync(Uri requestUrl)
        {
            try
            {
                return await _httpClient.GetAsync(requestUrl);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public void Dispose()
        {
            if (_disposalRunning)
            {
                // get out of the way if other thread is already running this.
                return;
            }

            // signal to other threads to get out of the way.
            _disposalRunning = true;

            // dispose all global disposables
            _httpClient.Dispose();

        }

        public async Task<string> GetResponseContentAsync(Uri requestUrl)
        {
            var logSb = new StringBuilder().Append("requestUrl: ").Append(requestUrl.AbsoluteUri).Append(Environment.NewLine);

            using var response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
            {
                logSb.Append("Request was not succesful.").Append(Environment.NewLine)
                    .Append("Media Wiki response status: ").Append(response.StatusCode);

                throw new ApplicationException(logSb.ToString());
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }

        public async Task<Stream> GetResponseContentStreamAsync(Uri requestUrl)
        {
            throw new NotImplementedException();
        }
    }
}
