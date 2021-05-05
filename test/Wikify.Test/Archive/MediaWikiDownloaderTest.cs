using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wikify.Archive;
using Wikify.Common;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.Network;
using Wikify.License;
using Wikify.License.Copyright;
using Wikify.Parsing.Content;

namespace Wikify.Test.Archive
{
    [TestClass]
    public class MediaWikiDownloaderTest
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
                new WikiMediaFactory());
        }

        [TestMethod]
        [DataRow("Giorgio_Moroder", LanguageEnum.English, TextContentModel.WikiText)]
        public async Task TestDownloadArticleAsync(string title, LanguageEnum language, TextContentModel contentModel)
        {
            var wikimedia = new WikiMediaFactory();
            var article = await _articleDownloader.GetArticleAsync(_articleIdentifierFactory.GetIdentifier(title, language), contentModel);
            ;
        }
    }
}
