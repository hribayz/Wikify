using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Wikify.Archive;
using Wikify.Common;

namespace Wikify.Test.Archive
{
    [TestClass]
    public class ArticleArchiveTest
    {
        IArticleArchive _articleArchive;
        public ArticleArchiveTest(IArticleArchive articleArchive)
        {
            _articleArchive = articleArchive;
        }
        [TestMethod]
        [DataRow("https://en.wikipedia.org/wiki/Laboratori_Nazionali_del_Gran_Sasso", "Laboratori Nazionali del Gran Sasso")]
        [DataRow("https://en.wikipedia.org/wiki/Direct-to-garment_printing", "Direct-to-garment printing")]
        public async Task GetArticle(string url, string title)
        {
            var articleIdentifier = CreateTestArticleIdentifier(url, title);
            var article = await _articleArchive.GetArticleHtmlAsync(articleIdentifier);

            ;
            // TODO : must contain certain substrings indicating success
        }
        private TestArticleIdentifier CreateTestArticleIdentifier(string url, string title) => new TestArticleIdentifier(url, title);
    }
}
