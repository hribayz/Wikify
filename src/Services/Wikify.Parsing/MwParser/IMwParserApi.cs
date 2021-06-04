using System.Threading.Tasks;
using Wikify.Common.Content;
using MwParserFromScratch.Nodes;
using System.Runtime.CompilerServices;
using Wikify.Parsing.Content;
using MwParserFromScratch;

[assembly: InternalsVisibleTo("Wikify.Test")]
namespace Wikify.Parsing.MwParser
{
    public interface IMwParserApi
    {
        /// <summary>
        /// Gets the Wikitext parser singleton.
        /// </summary>
        /// <returns>Wikitext parser singleton instance.</returns>
        public WikitextParser GetWikitextParser();
        public Task<Wikitext> GetArticleMwRootAsync(IWikiArticle wikiArticle);
        public Task<ArticleContainer> GetContainerAsync(IWikiArticle wikiArticle, Wikitext astRoot, IAstTranslator astTranslator, IWikiContentFactory wikiContentFactory);
    }
}
