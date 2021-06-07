using System.Threading.Tasks;
using Wikify.Common.Content;
using Microsoft.Extensions.Logging;
using Wikify.Parser.Content;

namespace Wikify.Parser.MwParser
{
    /// <summary>
    /// Implements the <see cref="IArticleParser"/> using the <see cref="IMwParserApi"/> service.
    /// </summary>
    public class ArticleParser : IArticleParser
    {
        private ILogger _logger;
        private IWikiContentFactory _wikiContentFactory;

        private IMwParserApi _mwParserApi;

        public ArticleParser(ILogger<ArticleParser> logger, IMwParserApi mwParserApi, IWikiContentFactory wikiContentFactory)
        {
            _logger = logger;
            _mwParserApi = mwParserApi;
            _wikiContentFactory = wikiContentFactory;
        }

        public async Task<IWikiContainer<IWikiArticle>> GetContainerAsync(IWikiArticle wikiArticle)
        {
            return await _mwParserApi.GetContainerAsync(wikiArticle, _wikiContentFactory);
        }
    }
}
