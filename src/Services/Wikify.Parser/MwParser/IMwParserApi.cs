using System.Threading.Tasks;
using Wikify.Common.Content;
using MwParserFromScratch.Nodes;
using System.Runtime.CompilerServices;
using Wikify.Parser.Content;
using MwParserFromScratch;

[assembly: InternalsVisibleTo("Wikify.Test")]
namespace Wikify.Parser.MwParser
{
    /// <summary>
    /// A facade over the MwParserFromScratch library. Encapsulates the <see cref="WikitextParser"/> instance and provides methods reducing its interface. 
    /// </summary>
    public interface IMwParserApi
    {
        /// <summary>
        /// Gets the <see cref="WikitextParser"/> singleton. Use this just for testing, otherwise direct calls to the parser should not be needed.
        /// </summary>
        /// <returns>Wikitext parser singleton instance.</returns>
        public WikitextParser GetWikitextParser();
        public Task<Wikitext> GetArticleMwRootAsync(IWikiArticle wikiArticle);
        public Task<IWikiContainer<IWikiArticle>> GetContainerAsync(IWikiArticle wikiArticle, IWikiContentFactory wikiContentFactory);
        public Task<IWikiContainer<IWikiArticle>> GetContainerAsync(IWikiArticle wikiArticle, Wikitext astRoot, IWikiContentFactory wikiContentFactory);
    }
}
