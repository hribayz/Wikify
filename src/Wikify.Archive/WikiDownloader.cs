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

namespace Wikify.Archive
{
    public class ArticleDownloader : IArchive<WikiArticle>
    {
        private HttpClient _httpClient;
        private Action _renewHttpClient;
        private ILogger _logger;
        private IContentParser<WikiArticle> _articleParser;

        public ArticleDownloader(IParserFactory parserFactory, INetworkingProvider networkingProvider, ILogger logger)
        {
            _logger = logger;  
            _httpClient = networkingProvider.GetHttpClient();
            _articleParser = parserFactory.GetHtmlParser<WikiArticle>();

            // assuming that the networkingProvider handles concurrency
            // no need to manage who and when triggers this
            _renewHttpClient += () => networkingProvider.GetHttpClient();
        }

        public async Task<IElement<WikiArticle>> GetElementAsync(IIdentifier<WikiArticle> articleIdentifier)
        {
            try
            {
                var url = articleIdentifier.GetUrl();
                var articleContentHtml = await _httpClient.GetStringAsync(url);
                var articleElement = await _articleParser.ParseContentAsync(articleContentHtml);

                return articleElement;
            }

            // TODO: only _renewHttpClient() when appropriate exception catched.
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                _renewHttpClient();
            }

            return null;
        }

        public async Task<System.Drawing.Image> GetImageAsync(IIdentifier<Common.Content.Types.WikiImage> imageIdentifier)
        {
            try
            {
                var url = imageIdentifier.GetUrl();
                var imageStream = await _httpClient.GetStreamAsync(url);
                var image = System.Drawing.Image.FromStream(imageStream, true);

                return image;
            }

            // TODO: only _renewHttpClient() when appropriate exception catched.
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                _renewHttpClient();
            }

            return null;
        }
    }
}
