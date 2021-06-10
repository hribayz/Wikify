using System;
using System.Threading.Tasks;
using MwParserFromScratch;
using MwParserFromScratch.Nodes;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Runtime.CompilerServices;
using Wikify.Parser.Content;
using Wikify.Common.Content.Raw;
using Wikify.Common.Content.Parsed;

[assembly: InternalsVisibleTo("Wikify.Test")]
namespace Wikify.Parser.MwParser
{
    public class MwParserApi : IMwParserApi
    {
        private ILogger _logger;
        private IAstParser _astTranslator;
        private IWikiContentFactory _wikiContentFactory;
        private IWikiComponentFactory _wikiComponentFactory;

        private WikitextParser _parser;

        public MwParserApi(ILogger<MwParserApi> logger, IAstParser astTranslator, IWikiContentFactory wikiContentFactory, IWikiComponentFactory wikiComponentFactory)
        {
            _logger = logger;
            _astTranslator = astTranslator;
            _wikiContentFactory = wikiContentFactory;
            _wikiComponentFactory = wikiComponentFactory;

            _parser = new WikitextParser();
        }
        public async Task<IWikiContainer<IWikiArticle>> GetContainerAsync(IWikiArticle wikiArticle)
        {
            var articleRoot = await GetArticleMwRootAsync(wikiArticle);
            return await GetContainerAsync(wikiArticle, articleRoot);
        }

        public async Task<Wikitext> GetArticleMwRootAsync(IWikiArticle wikiArticle)
        {
            #region Log article

            var articleDataString = wikiArticle.ArticleData.Substring(0, Math.Min(wikiArticle.ArticleData.Length, 50));
            _logger.LogInformation($"{nameof(GetArticleMwRootAsync)} parsing content:{Environment.NewLine}{articleDataString}");

            #endregion

            if (wikiArticle.ContentModel != ContentModel.WikiText)
            {
                var errorMessage = $"This implementation of {nameof(MwParserApi)} can only load an instance of {nameof(IWikiArticle)} with {ContentModel.WikiText} {nameof(ContentModel)}";
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

        public async Task<IWikiContainer<IWikiArticle>> GetContainerAsync(IWikiArticle wikiArticle, Wikitext astRoot)
        {
            var wikiData = _wikiContentFactory.CreateWikiData(astRoot.ToPlainText(), ContentModel.WikiText);

            // Create the root of WikiComponent tree.
            var articleContainer = _wikiComponentFactory.CreateArticleContainer(wikiData, wikiArticle, astRoot, astRoot);

            var firstChild = astRoot.Lines.FirstNode;

            // Compose WikiComponent tree.
            var rootChildren = await _astTranslator.TranslateNodesAsync(firstChild);

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
