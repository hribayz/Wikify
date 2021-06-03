using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Common.Network
{
    public interface INetworkingProvider : IDisposable
    {
        public Task<string> DownloadContentAsync(Uri contentUri);

        public Task<Image> DownloadImageAsync(Uri imageUri);
    }
}
