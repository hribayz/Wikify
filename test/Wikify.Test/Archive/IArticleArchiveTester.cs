using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Wikify.Archive;
using Wikify.Common;

namespace Wikify.Test.Archive
{

    public class IArticleArchiveTester
    {
        IArticleArchive _articleArchive;
        public IArticleArchiveTester(IArticleArchive articleArchive)
        {
            _articleArchive = articleArchive;
        }
        public async Task<string> GetArticleAsync(IArticleIdentifier articleIdentifier)
        {
            return await _articleArchive.GetArticleHtmlAsync(articleIdentifier);
        }
    }
}
