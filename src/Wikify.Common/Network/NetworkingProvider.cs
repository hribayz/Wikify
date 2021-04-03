using System.Net.Http;

namespace Wikify.Common.Network
{
    public class NetworkingProvider : INetworkingProvider
    {
        private HttpClient _httpClient;
        private bool _disposalRunning;

        public NetworkingProvider()
        {
            _disposalRunning = false;

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
            // TODO: check if http client OK. Client code might have broken or disposed it.
            return _httpClient;
        }

        public void Dispose()
        {
            if (_disposalRunning)
            {
                // get out of the way if other thread just performs this.
                return;
            }

            // signal to other threads to get out of the way.
            _disposalRunning = true;

            // dispose all global disposables
            _httpClient.Dispose();

        }
    }
}
