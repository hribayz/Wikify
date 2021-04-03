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
using Wikify.Common.Id;
using Wikify.Common.Network;
using Wikify.License;
using Wikify.Parsing.Content;

namespace Wikify.Archive.AngleSharp
{
    public class ArticleDownloader : AngleSharpDownloaderBase, IArchive<WikiArticle>
    {
        private readonly IElementValidator _elementValidator;
        private readonly ILicenseProvider _licenseProvider;

        public ArticleDownloader(
            ILogger logger,
            INetworkingProvider networkingProvider,
            IElementValidator elementValidator,
            ILicenseProvider licenseProvider) : base(logger, networkingProvider)
        {
            _elementValidator = elementValidator;
            _licenseProvider = licenseProvider;
        }

        public async Task<IWikiContainer<WikiArticle>> GetElementAsync(IContentIdentifier<WikiArticle> articleIdentifier, RetrievalOptions retrievalOptions)
        {
            try
            {
                var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());

                // TODO : download using networking provider

                var document = await context.OpenAsync(articleIdentifier.GetUrl()) as IElement;



                return new ArticleContainer(document);
            }

            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        private IWikiComponent? GetComponent(IElement element)
        {
            var tagName = element.TagName;
            var id = element.Id;
            var classes = element.ClassList;

            if (_elementValidator.IsValidElement(tagName, id, classes))
            {
                WikiComponent wikiComponent = new WikiComponent(element, _licenseProvider.GetLicense())
            }

            foreach (var child in element.Children)
            {

            }


        }
    }
}
