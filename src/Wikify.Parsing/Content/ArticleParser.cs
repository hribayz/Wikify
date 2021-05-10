using System;
using System.IO;
using System.Threading.Tasks;
using Wikify.Common.Content;
using MwParserFromScratch;
using MwParserFromScratch.Nodes;
using Microsoft.Extensions.Logging;

namespace Wikify.Parsing.Content
{
    /// <summary>
    /// 
    /// </summary>
    public class ArticleParser : IWikiArticleParser
    {
        private ILogger _logger;

        public ArticleParser(ILogger logger)
        {
            _logger = logger;
        }

        public Task<IWikiContainer<IWikiArticle>> GetContainerAsync(IWikiArticle wikiArticle)
        {
            if (wikiArticle.ContentModel != TextContentModel.WikiText)
            {
                throw new NotSupportedException($"This implementation of {nameof(ArticleParser)} can only load an instance of {nameof(IWikiArticle)} with {TextContentModel.WikiText} {nameof(TextContentModel)}");
            }

            var parser = new WikitextParser();
            var ast = parser.Parse(wikiArticle.ArticleData);

            
        }
    }
}
