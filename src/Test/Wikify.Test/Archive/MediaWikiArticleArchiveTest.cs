using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wikify.Archive;
using Wikify.Common.Content.Raw;
using Wikify.Common.Domain;
using Wikify.Common.Id;
using Wikify.Common.Network;
using Wikify.License;
using Wikify.License.Copyright;

namespace Wikify.Test.Archive
{
    [TestClass]
    public class MediaWikiArticleArchiveTest
    {
        private static ILoggerFactory _loggerFactory;
        private static IArticleArchive _articleDownloader;
        private static IArticleIdentifierFactory _articleIdentifierFactory;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _loggerFactory = new LoggerFactory();
            _articleIdentifierFactory = new ArticleIdentifierFactory();
            _articleDownloader = new MediaWikiArticleDownloader(
                _loggerFactory.CreateLogger<MediaWikiArticleDownloader>(),
                new Wikify.Common.Network.NetworkingProvider(_loggerFactory.CreateLogger<NetworkingProvider>()),
                new ArticleLicenseProvider(_loggerFactory.CreateLogger<ArticleLicenseProvider>(), new LicenseFactory(), new CopyrightFactory(new CopyrightResolver())),
                new WikiContentFactory());
        }

        [TestMethod]
        [DataRow("Giorgio Moroder", LanguageEnum.English, ContentModel.WikiText, "birth_name = Giovanni Giorgio Moroder")]
        public async Task TestDownloadArticleAsync(string title, LanguageEnum language, ContentModel contentModel, string mustHaveSubstring)
        {
            var article = await _articleDownloader.GetArticleAsync(_articleIdentifierFactory.GetIdentifier(title, language), contentModel);

            Assert.IsTrue(article.ArticleData.Contains(mustHaveSubstring));
        }
    }
}
