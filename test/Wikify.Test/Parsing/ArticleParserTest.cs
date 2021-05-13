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
        private static INetworkingProvider _networkingProvider;
        private static IArticleLicenseProvider _articleLicenseProvider;
        private static ILicenseFactory _licenseFactory;
        private static ICopyrightResolver _copyrightResolver;
        private static ICopyrightFactory _copyrightFactory;
        private static IWikiMediaFactory _wikiMediaFactory;
        private static IAstTranslator _astTranslator;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _loggerFactory = new LoggerFactory();
            _articleIdentifierFactory = new ArticleIdentifierFactory();
            _licenseFactory = new LicenseFactory();
            _copyrightResolver = new CopyrightResolver();
            _wikiMediaFactory = new WikiMediaFactory();

            _networkingProvider = new NetworkingProvider(
                _loggerFactory.CreateLogger<NetworkingProvider>());

            _copyrightFactory = new CopyrightFactory(
                _copyrightResolver);

            _articleLicenseProvider = new ArticleLicenseProvider(
                _loggerFactory.CreateLogger<ArticleLicenseProvider>(),
                _licenseFactory,
                _copyrightFactory);

            _articleDownloader = new MediaWikiArticleDownloader(
                _loggerFactory.CreateLogger<MediaWikiArticleDownloader>(),
                _networkingProvider,
                _articleLicenseProvider,
                _wikiMediaFactory);

            _astTranslator = new PatternMatchingProvider(
                _loggerFactory.CreateLogger<PatternMatchingProvider>());

            _articleParser = new ArticleParser(
                _loggerFactory.CreateLogger<ArticleParser>(),
                _astTranslator);
        }



        [TestMethod]
        [DataRow("Edinburgh")]
        public async Task TestParseArticle(string title)
        {
            var article = await _articleDownloader.GetArticleAsync(_articleIdentifierFactory.GetIdentifier(title, Common.LanguageEnum.English), TextContentModel.WikiText);

            await _articleParser.GetContainerAsync(article);

            ;
        }

        //private WikiArticle CreateWikiArticle(string )
    }
}
