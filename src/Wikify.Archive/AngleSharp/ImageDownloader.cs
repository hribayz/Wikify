using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Content.Types;
using Wikify.Common.Id;

namespace Wikify.Archive.AngleSharp
{
    public class ImageDownloader : IArchive<WikiImage>
    {
        private HttpClient _httpClient;
        private Action _renewClient;

        public async Task<IContainer<WikiImage>> GetElementAsync(IIdentifier<WikiImage> imageIdentifier, RetrieveOptions retrieveOptions)
        {
            try
            {
                var url = imageIdentifier.GetUrl();
                var imageStream = await _httpClient.GetStreamAsync(url);
                var image = Image.FromStream(imageStream, true);

                return new ImageContainer();
            }

            // TODO: try to recover first by renewing client when possible

            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
    }
}
