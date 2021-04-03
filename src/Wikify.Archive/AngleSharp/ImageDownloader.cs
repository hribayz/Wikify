using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.Network;

namespace Wikify.Archive.AngleSharp
{
    public class ImageDownloader : AngleSharpDownloaderBase, IArchive<Common.Content.WikiImage>
    {
        public ImageDownloader(ILogger logger, INetworkingProvider networkingProvider) : base(logger, networkingProvider)
        {

        }

        public async Task<IWikiContainer<Common.Content.WikiImage>> GetElementAsync(IContentIdentifier<Common.Content.WikiImage> imageIdentifier, RetrievalOptions retrieveOptions)
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
