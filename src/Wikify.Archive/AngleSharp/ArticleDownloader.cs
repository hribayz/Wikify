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
    public class ArticleDownloader : AngleSharpDownloaderBase, IArchive<WikiArticle>
    {
        public ArticleDownloader(ILogger logger, INetworkingProvider networkingProvider) : base(logger, networkingProvider)
        {

        }

        public async Task<IContainer<WikiArticle>> GetElementAsync(IIdentifier<WikiArticle> articleIdentifier, RetrieveOptions retrieveOptions)
        {
            try
            {
                var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());

                // TODO : download using networking provider

                var document = await context.OpenAsync(articleIdentifier.GetUrl());

                return new ArticleContainer(document);
            }

            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

    }
}
