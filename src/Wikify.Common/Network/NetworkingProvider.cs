using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
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

        /// Encapsulate HttpResponse instantiation and disposal to avoid passing ownership of IDisposables.
        private async Task<T> GetResponseAsAsync<T>(Uri requestUri, Func<HttpResponseMessage, Task<T>> projection)
        {
            var logSb = new StringBuilder().Append("requestUrl: ").Append(requestUri.AbsoluteUri).Append(Environment.NewLine);

            // Make sure the projection result is in memory before disposing the response as that disposes all its resources as well
            // so e.g. Content.ReadStreamAsync() will not be possible on that instance.
            T projectionResult;

            using (var response = await _httpClient.GetAsync(requestUri))
            {
                if (!response.IsSuccessStatusCode)
                {
                    logSb.Append("Request was not succesful.").Append(Environment.NewLine)
                        .Append("Response status: ").Append(response.StatusCode);

                    throw new ApplicationException(logSb.ToString());
                }

                projectionResult = await projection(response);
            }

            return projectionResult;
        }

        public async Task<string> DownloadContentAsync(Uri contentUri)
        {
            return await GetResponseAsAsync(
                contentUri, 
                async httpResponseMessage => await httpResponseMessage.Content.ReadAsStringAsync());
        }

        public async Task<Image> DownloadImageAsync(Uri imageUri)
        {
            return await GetResponseAsAsync(
                imageUri,
                async httpResponseMessage => Image.FromStream(await httpResponseMessage.Content.ReadAsStreamAsync()));
        }
        public void Dispose()
        {
            if (_disposalRunning)
            {
                return;
            }

            _disposalRunning = true;

            _httpClient.Dispose();
        }

    }
}
