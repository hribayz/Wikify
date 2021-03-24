using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wikify.Archive;

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
        public void GetImage(IArticleArchive wikiArticleArchive)
        {
        }
    }

}
