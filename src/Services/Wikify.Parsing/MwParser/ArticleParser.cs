using System.IO;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Microsoft.Extensions.Logging;
using Wikify.Parsing.Content;
using MwParserFromScratch;

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

        private IMwParserApi _mwParserApi;

        public ArticleParser(ILogger<ArticleParser> logger, IMwParserApi mwParserApi, IAstTranslator astTranslator, IWikiContentFactory wikiContentFactory)
        {
            _logger = logger;
            _mwParserApi = mwParserApi;
            _astTranslator = astTranslator;
            _wikiContentFactory = wikiContentFactory;
        }

        public async Task<IWikiContainer<IWikiArticle>> GetContainerAsync(IWikiArticle wikiArticle)
        {
            var articleRoot = await _mwParserApi.GetArticleMwRootAsync(wikiArticle);
            return await _mwParserApi.GetContainerAsync(wikiArticle, articleRoot, _astTranslator, _wikiContentFactory);
        }
    }
}
