using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Wikify.Parser.Content;
using Wikify.Common.Content.Raw;
using Wikify.Common.Content.Parsed;

namespace Wikify.Parser.MwParser
{
    /// <summary>
    /// Implements the <see cref="IArticleParser"/> using the <see cref="IMwParserApi"/> service.
    /// </summary>
    public class ArticleParser : IArticleParser
    {
        private ILogger _logger;
        private Content.IWikiComponentFactory _wikiContentFactory;

        private IMwParserApi _mwParserApi;

        public ArticleParser(ILogger<ArticleParser> logger, IMwParserApi mwParserApi, Content.IWikiComponentFactory wikiContentFactory)
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
