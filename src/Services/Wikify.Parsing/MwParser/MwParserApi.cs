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

    public class MwParserApi : IMwParserApi
    {
        private ILogger _logger;

        private WikitextParser _parser;

        public MwParserApi(ILogger<MwParserApi> logger)
        {
            _logger = logger;

            _parser = new WikitextParser();
        }

        public async Task<Wikitext> GetArticleMwRootAsync(IWikiArticle wikiArticle)
        {
            #region Log article

            var articleDataString = wikiArticle.ArticleData.Substring(0, Math.Min(wikiArticle.ArticleData.Length, 50));
            _logger.LogInformation($"{nameof(GetArticleMwRootAsync)} parsing content:{Environment.NewLine}{articleDataString}");

            #endregion

            if (wikiArticle.ContentModel != TextContentModel.WikiText)
            {
                var errorMessage = $"This implementation of {nameof(MwParserApi)} can only load an instance of {nameof(IWikiArticle)} with {TextContentModel.WikiText} {nameof(TextContentModel)}";
                _logger.LogError(errorMessage);
                throw new NotSupportedException(errorMessage);
            }

            // Build article AST
            _logger.LogDebug("Building article AST...");

            var astRoot = await Task.Run(
                () => _parser.Parse(wikiArticle.ArticleData));

            _logger.LogDebug("Done.");

            if (astRoot == null)
            {
                var errorMessage = $"{nameof(WikitextParser)} returned null AST root.";
                _logger.LogError(errorMessage);
                throw new ApplicationException(errorMessage);
            }

            return astRoot;
        }

        public async Task<ArticleContainer> GetContainerAsync(IWikiArticle wikiArticle, Wikitext astRoot, IAstTranslator astTranslator, IWikiContentFactory wikiContentFactory)
        {
            // Create the root of WikiComponent tree.
            var articleContainer = wikiContentFactory.CreateArticle(wikiArticle, astRoot, astRoot);

            var firstChild = astRoot.Lines.FirstNode;

            // Compose WikiComponent tree.
            var rootChildren = await astTranslator.TranslateNodesAsync(firstChild);

            if (rootChildren.Any())
            {
                articleContainer.AddChildren(rootChildren);
            }
            else
            {
                _logger.LogWarning("AST translator returned empty list of article descendants.");
            }

            return articleContainer;
        }

        public WikitextParser GetWikitextParser()
        {
            return _parser;
        }
    }
}
