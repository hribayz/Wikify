using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wikify.Archive;

namespace Wikify.Sample.Test
{
    [TestClass]
    public class ArticleArchiveTest
    {
        IArticleArchive _articleArchive;
        public ArticleArchiveTest(IArticleArchive wikiArticleArchive)
        {
            _articleArchive = articleArchive;
        }
        [TestMethod]
        public void GetImage(IArticleArchive wikiArticleArchive)
        {
        }
    }
}
