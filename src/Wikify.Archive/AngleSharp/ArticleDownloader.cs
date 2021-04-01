using AngleSharp;
using AngleSharp.Dom;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common;
using Wikify.Common.Content;
using Wikify.Common.Content.Types;
using Wikify.Common.Id;
using Wikify.Common.Network;
using Wikify.Parsing.Content;

namespace Wikify.Archive.AngleSharp
{
    public class ArticleDownloader : IArchive<WikiArticle>
    {
        private ILogger _logger;
        private HttpClient _httpClient;
        private Action _renewClient;

        public ArticleDownloader(ILogger logger, INetworkingProvider networkingProvider)
        {
            _logger = logger;
            _httpClient = networkingProvider.GetHttpClient();
            _renewClient = () => networkingProvider.GetHttpClient();
        }

        public async Task<IContainer<WikiArticle>> GetElementAsync(IIdentifier<WikiArticle> articleIdentifier)
        {
            try
            {
                var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
                var document = await context.OpenAsync(articleIdentifier.GetUrl());

                return new ArticleContainer(document);
            }

            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public Task<IContainer<WikiArticle>> GetElementAsync(IIdentifier<WikiArticle> elementIdentifier, RetrieveOptions retrieveOptions)
        {
            throw new NotImplementedException();
        }

        public async Task<Image> GetImageAsync(IIdentifier<WikiImage> imageIdentifier)
        {
            try
            {
                var url = imageIdentifier.GetUrl();
                var imageStream = await _httpClient.GetStreamAsync(url);
                var image = Image.FromStream(imageStream, true);

                return image;
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
