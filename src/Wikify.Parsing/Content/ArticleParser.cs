using System;
using System.IO;
using System.Threading.Tasks;
using Wikify.Common.Content;
using MwParserFromScratch;
using MwParserFromScratch.Nodes;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Wikify.Parsing.Content
{
    /// <summary>
    /// 
    /// </summary>
    public class ArticleParser : IWikiArticleParser
    {
        private ILogger _logger;
        private IAstTranslator _astTranslator;
        private IWikiContentFactory _wikiContentFactory;

        private WikitextParser _parser;

        public ArticleParser(ILogger logger, IAstTranslator astTranslator, IWikiContentFactory wikiContentFactory)
        {
            _logger = logger;
            _astTranslator = astTranslator;
            _wikiContentFactory = wikiContentFactory;

            _parser = new WikitextParser();
        }

        public async Task<IWikiContainer<IWikiArticle>> GetContainerAsync(IWikiArticle wikiArticle)
        {
            if (wikiArticle.ContentModel != TextContentModel.WikiText)
            {
                var errorMessage = $"This implementation of {nameof(ArticleParser)} can only load an instance of {nameof(IWikiArticle)} with {TextContentModel.WikiText} {nameof(TextContentModel)}";
                _logger.LogError(errorMessage);
                throw new NotSupportedException(errorMessage);
            }

            _logger.LogDebug($"{nameof(GetContainerAsync)} parsing content:{Environment.NewLine}{wikiArticle}");

            // Build article AST
            _logger.LogInformation("Building article AST...");
            var astRoot = _parser.Parse(wikiArticle.ArticleData);
            _logger.LogInformation("Done.");

            if (astRoot == null)
            {
                var errorMessage = $"{nameof(WikitextParser)} returned null AST root.";
                _logger.LogError(errorMessage);
                throw new ApplicationException(errorMessage);
            }

            // Create the root of WikiComponent tree.
            var articleContainer = _wikiContentFactory.CreateArticle(wikiArticle, astRoot, astRoot);

            // Compose WikiComponent tree.
            var baseComponents = await _astTranslator.TranslateNodesAsync(astRoot);

            articleContainer.AddChildren(baseComponents.First ??
                throw new ApplicationException($"{nameof(_astTranslator)} returned empty tree. No match at all?"));

            // Make sure that there is a single article component in the composition.

            return articleContainer;
        }
    }
}
