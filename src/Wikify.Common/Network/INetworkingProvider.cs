using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Common.Network
{
    public interface INetworkingProvider : IDisposable
    {
        public Task<string> GetResponseContentAsync(Uri requestUrl);
        public Task<Stream> GetResponseContentStreamAsync(Uri requestUrl);
    }
}
