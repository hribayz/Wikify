using System.Threading.Tasks;
using Wikify.Common;

namespace Wikify.Archive
{
    public interface IArticleArchive
    {
        public Task<string> GetArticleHtmlAsync(IArticleIdentifier articleIdentifier);
    }
    public class WikiArticleProvider : IArticleArchive
    {
        public Task<string> GetArticleHtmlAsync(IArticleIdentifier articleIdentifier)
        {
            throw new System.NotImplementedException();
        }
    }
}