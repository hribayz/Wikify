using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content;

namespace Wikify.Parsing.Content
{
    // TODO : enforce functional implementation as in ILicenseProvider by returning parsed container with accessors
    public interface IWikiArticleParser
    {
        public Task<IWikiContainer<IWikiArticle>> GetContainerAsync(IWikiArticle wikiArticle);
    }
    public interface IWikiComponentParser
    {
        public Task<DirectoryInfo> GetArticleHtmlAndSaveAsync(IWikiContainer<IWikiArticle> articleContainer, IArticleParseOptions articleParseOptions);
    }
    public interface IArticleParseOptions
    {

    }
}
