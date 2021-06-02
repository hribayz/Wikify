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
        private async Task<IWikiContainer<IWikiArticle>> GetArticleContainerAsync(string title)
        {
            var articleDownloader = GetService<IArticleArchive>();
            var articleIdentifierFactory = GetService<IArticleIdentifierFactory>();
            var articleParser = GetService<IArticleParser>();

            var articleArchive = await articleDownloader.GetArticleAsync(articleIdentifierFactory.GetIdentifier(title, Common.LanguageEnum.English), TextContentModel.WikiText);
            return await articleParser.GetContainerAsync(articleArchive);
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
        #region Data rows

        [DataRow("Morgan Mason")]
        [DataRow("Edinburgh")]
        [DataRow("National Library of Scotland")]
        [DataRow("House of Lords")]
        [DataRow("USS Oriskany (CV-34)")]
        [DataRow("Midway Atoll")]
        [DataRow("Tsunami")]
        [DataRow("Delphi")]
        [DataRow("2011 Tōhoku earthquake and tsunami")]
        [DataRow("Academia Sinica")]
        [DataRow("Chinese Academy of Sciences")]
        [DataRow("Olympia, Greece")]
        [DataRow("History of the Peloponnesian War")]

        #endregion
        public async Task TestArticleHasInfoPanelAsync(string title)
        {
            var articleContainer = await GetArticleContainerAsync(title);
            var children = articleContainer.GetChildren();
            var infoPanels = articleContainer.GetChildren(x => x.ComponentType == WikiComponentType.InfoPanel);
            Assert.IsTrue(infoPanels.Any());
        }

    }
}
