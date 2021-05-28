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
    public class ArticleParserTest : WikifyTestBase
    {
        private static IArticleIdentifierFactory _articleIdentifierFactory;
        private static IArticleArchive _articleDownloader;
        private static IArticleParser _articleParser;

        [ClassInitialize]
        public void ClassInitialize()
        {
            _articleIdentifierFactory = GetService<IArticleIdentifierFactory>();
            _articleDownloader = GetService<IArticleArchive>();
            _articleParser = GetService<IArticleParser>();
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
            var children = articleContainer.GetChildren();
            var infoPanels = articleContainer.GetChildren(x => x.ComponentType == WikiComponentType.InfoPanel);
            Assert.IsTrue(infoPanels.SingleOrDefault() != null);
        }

    }
}
