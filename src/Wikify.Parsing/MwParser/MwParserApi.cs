using System;
using System.Threading.Tasks;
using Wikify.Common.Content;
using MwParserFromScratch;
using MwParserFromScratch.Nodes;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Runtime.CompilerServices;
using Wikify.Parsing.Content;

[assembly: InternalsVisibleTo("Wikify.Test")]
namespace Wikify.Parsing.MwParser
{
    internal class MwParserApi
    {
        private ILogger _logger;

        private WikitextParser _parser;

        public MwParserApi(ILogger logger)
        {
            _logger = logger;

            _parser = new WikitextParser();
        }

        internal async Task<Wikitext> GetArticleMwRoot(IWikiArticle wikiArticle)
        {
            _logger.LogDebug($"{nameof(GetArticleMwRoot)} parsing content:{Environment.NewLine}{wikiArticle.ArticleData}");

            if (wikiArticle.ContentModel != TextContentModel.WikiText)
            {
                var errorMessage = $"This implementation of {nameof(MwParserApi)} can only load an instance of {nameof(IWikiArticle)} with {TextContentModel.WikiText} {nameof(TextContentModel)}";
                _logger.LogError(errorMessage);
                throw new NotSupportedException(errorMessage);
            }

            // Build article AST
            _logger.LogInformation("Building article AST...");

            var astRoot = await Task.Run(
                () => _parser.Parse(wikiArticle.ArticleData));

            _logger.LogInformation("Done.");

            if (astRoot == null)
            {
                var errorMessage = $"{nameof(WikitextParser)} returned null AST root.";
                _logger.LogError(errorMessage);
                throw new ApplicationException(errorMessage);
            }

            return astRoot;
        }

        internal async Task<ArticleContainer> GetContainerAsync(IWikiArticle wikiArticle, Wikitext astRoot, IAstTranslator astTranslator, IWikiContentFactory wikiContentFactory)
        {
            // Create the root of WikiComponent tree.
            var articleContainer = wikiContentFactory.CreateArticle(wikiArticle, astRoot, astRoot);

            var firstChild = astRoot.Lines.FirstNode;

            // Compose WikiComponent tree.
            var baseComponents = await astTranslator.TranslateNodesAsync(firstChild);

            if (baseComponents.Any())
            {
                articleContainer.AddChildren(baseComponents.First ??
                    throw new ApplicationException($"{nameof(astTranslator)} returned null node."));
            }

            return articleContainer;
        }
    }
}
