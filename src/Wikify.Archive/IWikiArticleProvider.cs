using System.Threading.Tasks;
using Wikify.Common;

namespace Wikify.Archive
{
    interface IWikiArticleArchive
    {
        public Task<string> GetArticleHtmlAsync(AWikiArticleIdentifier articleIdentifier);
    }
    public class WikiArticleProvider : IWikiArticleArchive
    {
        public Task<string> GetArticleHtmlAsync(AWikiArticleIdentifier articleIdentifier)
        {
            throw new System.NotImplementedException();
        }
    }
}