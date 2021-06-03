using System.IO;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Microsoft.Extensions.Logging;
using Wikify.Parsing.Content;

namespace Wikify.Parsing.MwParser
{
    /// <summary>
    /// 
    /// </summary>
    public class ArticleParser : IArticleParser
    {
        private ILogger _logger;
        private IAstTranslator _astTranslator;
        private IWikiContentFactory _wikiContentFactory;

        private MwParserApi _mwParserApi;

        public ArticleParser(ILogger<ArticleParser> logger, IAstTranslator astTranslator, IWikiContentFactory wikiContentFactory)
        {
            _logger = logger;
            _astTranslator = astTranslator;
            _wikiContentFactory = wikiContentFactory;

            _mwParserApi = new MwParserApi(logger);
        }

        public async Task<IWikiContainer<IWikiArticle>> GetContainerAsync(IWikiArticle wikiArticle)
        {
            var articleRoot = await _mwParserApi.GetArticleMwRoot(wikiArticle);
            return await _mwParserApi.GetContainerAsync(wikiArticle, articleRoot, _astTranslator, _wikiContentFactory);
        }
    }
}
