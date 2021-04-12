using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content;

namespace Wikify.Parsing.Content
{
    // TODO : enforce functional implementation as in ILicenseProvider by returning parsed container with accessors
    public interface IArticleParser
    {
        public Task LoadArticleAsync(IWikiArticle article);
        public Task LoadBuildOptionsAsync(BuildOptions buildOptions);
        public Task<DirectoryInfo> CreateHtmlAsync();
        public Task<IWikiContainer<IWikiArticle>> GetArticleAsync();
    }
}
