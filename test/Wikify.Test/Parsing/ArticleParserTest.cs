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
using Wikify.Parsing.MwParser;

namespace Wikify.Test.Parsing
{
    [TestClass]
    public class ArticleParserTest
    {
        private static ILoggerFactory _loggerFactory;
        private static IArticleIdentifierFactory _articleIdentifierFactory;
        private static IArticleArchive _articleDownloader;
        private static INetworkingProvider _networkingProvider;
        private static IArticleLicenseProvider _articleLicenseProvider;
        private static ILicenseFactory _licenseFactory;
        private static ICopyrightResolver _copyrightResolver;
        private static ICopyrightFactory _copyrightFactory;
        private static IWikiMediaFactory _wikiMediaFactory;
        private static IAstTranslator _astTranslator;
        private static IWikiContentFactory _wikiContentFactory;


        private static ArticleParser _articleParser;

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

            _wikiContentFactory = new WikiContentFactory();

            _astTranslator = new MwAstTranslator(
                _loggerFactory.CreateLogger<MwAstTranslator>(),
                _wikiContentFactory);

            _articleParser = new ArticleParser(
                _loggerFactory.CreateLogger<ArticleParser>(),
                _astTranslator,
                _wikiContentFactory);
        }


        private async Task<IWikiContainer<IWikiArticle>> GetArticleContainerAsync(string title)
        {
            var articleArchive = await _articleDownloader.GetArticleAsync(_articleIdentifierFactory.GetIdentifier(title, Common.LanguageEnum.English), TextContentModel.WikiText);
            return await _articleParser.GetContainerAsync(articleArchive);
        }


        [TestMethod]
        [DataRow("Edinburgh")]
        public async Task TestArticleHasSingleLeadSectionAsync(string title)
        {
            var articleContainer = await GetArticleContainerAsync(title);
            Assert.IsTrue(articleContainer.GetChildren(x => x.ComponentType == WikiComponentType.LeadSection).SingleOrDefault() != null);
        }

        [TestMethod]
        [DataRow("List of Iron Maiden band members")]
        public async Task TestBandLineupHasSingleTimelineAsync(string title)
        {
            var articleContainer = await GetArticleContainerAsync(title);
            Assert.IsTrue(articleContainer.GetChildren(x => x.ComponentType == WikiComponentType.BandLineupTimeline).SingleOrDefault() != null);
        }

        [TestMethod]
        [DataRow("Morgan Mason")]
        public async Task TestArticleHasSingleInfoPanelAsync(string title)
        {
            var articleContainer = await GetArticleContainerAsync(title);
            Assert.IsTrue(articleContainer.GetChildren(x => x.ComponentType == WikiComponentType.InfoPanel).SingleOrDefault() != null);
        }

    }
}
