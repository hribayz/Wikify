using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Wikify.Archive;
using Wikify.Common.Content.Types;
using Wikify.Common.Network;
using Wikify.Test.Archive;

namespace Wikify.Test.Sample
{
    [TestClass]
    public class SampleArchiveTest
    {
        private static IArchive<WikiArticle> _articleArchive;

        //[ClassInitialize]
        //public static void ClassInitialize(TestContext context)
        //{
        //    _articleArchive = new ArticleDownloader(,new NetworkingProvider(), NullLogger.Instance);
        //}

        [TestMethod]
        [DataRow("https://en.wikipedia.org/wiki/Laboratori_Nazionali_del_Gran_Sasso", "Laboratori Nazionali del Gran Sasso")]
        [DataRow("https://en.wikipedia.org/wiki/Direct-to-garment_printing", "Direct-to-garment printing")]
        public async Task GetArticle(string url, string title)
        {
            var articleIdentifier = CreateTestArticleIdentifier(url, title);
            //var article = await _articleArchive.GetArticleHtmlAsync(articleIdentifier);

            ;
            // TODO : must contain certain substrings indicating success
        }
        private TestArticleIdentifier CreateTestArticleIdentifier(string url, string title) => new TestArticleIdentifier(url, title);
    }

}
