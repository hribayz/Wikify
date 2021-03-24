using System.Threading.Tasks;
using Wikify.Common;

namespace Wikify.Archive
{
    public interface IArticleArchive
    {
        public Task<string> GetArticleHtmlAsync(AArticleIdentifier articleIdentifier);
    }
    public class WikiArticleProvider : IArticleArchive
    {
        public Task<string> GetArticleHtmlAsync(AArticleIdentifier articleIdentifier)
        {
            throw new System.NotImplementedException();
        }
    }
}