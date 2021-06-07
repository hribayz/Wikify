using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using Wikify.Archive;
using Wikify.Generators;
using Wikify.Parser.MwParser;
using static Wikify.Parser.MwParser.NodeExtensions;

namespace Wikify.Test.Parsing
{
    [TestClass]
    public class NodeExtensionsTests : WikifyTestBase
    {
        [TestMethod]
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
        public async Task TestTemplateHasNameAsync(string title)
        {
            var patternMatchingService = GetService<IPatternMatchingService>();
            var mwParserApi = GetService<IMwParserApi>();
            var article = await GetArticleContainerAsync(title);

            var nodeGenerator = new NodeGenerator(mwParserApi.GetWikitextParser());

            var templates = nodeGenerator.GetTemplatesFromNode(await mwParserApi.GetArticleMwRootAsync(article.Content));

            var templateNames = templates.Select(x => x.GetTemplateName());

            Assert.IsTrue(templateNames.All(x => !string.IsNullOrWhiteSpace(x)));
        }
    }
}
