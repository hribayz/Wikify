using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Archive;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.Network;
using Wikify.License;
using Wikify.License.Copyright;
using Wikify.Parsing.Content;

namespace Wikify.Test.Parsing
{
    [TestClass]
    public class ArticleParserTest
    {
        private static ILoggerFactory _loggerFactory;
        private static IArticleIdentifierFactory _articleIdentifierFactory;
        private static IArticleArchive _articleDownloader;
        private static IWikiArticleParser _articleParser;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _loggerFactory = new LoggerFactory();
            _articleIdentifierFactory = new ArticleIdentifierFactory();
            _articleDownloader = new MediaWikiArticleDownloader(_loggerFactory.CreateLogger<MediaWikiArticleDownloader>(),
                new NetworkingProvider(_loggerFactory.CreateLogger<NetworkingProvider>()),
                new ArticleLicenseProvider(_loggerFactory.CreateLogger<ArticleLicenseProvider>(), 
                    new LicenseFactory(),
                    new CopyrightFactory(
                        new CopyrightResolver())),
                new WikiMediaFactory());
            _articleParser = new ArticleParser(_loggerFactory.CreateLogger<ArticleParser>());
        }



        [TestMethod]
        [DataRow("Edinburgh")]
        public async Task TestParseArticle(string title)
        {
            var article = await _articleDownloader.GetArticleAsync(_articleIdentifierFactory.GetIdentifier(title, Common.LanguageEnum.English), TextContentModel.WikiText);

            await _articleParser.LoadArticleAsync(article);

            ;
        }

        //private WikiArticle CreateWikiArticle(string )
    }
}
