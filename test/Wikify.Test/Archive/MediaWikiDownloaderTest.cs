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
using Wikify.License;
using Wikify.Parsing.Content;

namespace Wikify.Test.Archive
{
    [TestClass]
    public class MediaWikiDownloaderTest
    {
        private static LoggerFactory _loggerFactory;
        private static IArticleArchive _articleDownloader;
        private static IArticleIdentifierFactory _articleIdentifierFactory;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _loggerFactory = new LoggerFactory();
            _articleIdentifierFactory = new ArticleIdentifierFactory();
            _articleDownloader = new MediaWikiDownloader(
                _loggerFactory.CreateLogger<MediaWikiDownloader>(),
                new Wikify.Common.Network.NetworkingProvider(),
                new LicenseProvider(),
                new WikiMediaFactory());
        }

        [TestMethod]
        [DataRow("Giorgio Moroder", LanguageEnum.English, WikiContentModel.WikiText)]
        public async Task TestDownloadArticleAsync(string title, LanguageEnum language, WikiContentModel contentModel)
        {
            var article = await _articleDownloader.GetArticleAsync(_articleIdentifierFactory.GetIdentifier(title, language), contentModel);
            ;
        }
    }
}
