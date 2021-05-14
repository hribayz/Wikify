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
        private WikitextParser _parser;

        public ArticleParser(ILogger logger, IAstTranslator astTranslator)
        {
            _logger = logger;
            _astTranslator = astTranslator;
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

            var baseComponents = await _astTranslator.TranslateNodesAsync(astRoot);
            var articleContainer = baseComponents.SingleOrDefault(x => x.ComponentType == WikiComponentType.Article);

            // Make sure that there is a single article component in the composition.
            if (articleContainer == null)
            {
                var errorMessage = $"{nameof(GetContainerAsync)} encountered an {nameof(IWikiArticle)} with no or more than one base component of type {WikiComponentType.Article}";
                _logger.LogError(errorMessage);
                throw new ApplicationException(errorMessage);
            }

            return (IWikiContainer<IWikiArticle>)articleContainer;
        }
    }
}
