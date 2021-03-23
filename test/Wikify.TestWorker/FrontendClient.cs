using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Wikify.Archive;
using Wikify.Common;
using Wikify.Display;
using Wikify.Parsing;

namespace Wikify.TestWorker
{
    [TestClass]
    public class FrontendClient
    {
        [TestMethod]
        public async Task FullRoutineAsync()
        {
            var wikiObjectParser = new WikiObjectParser();
            var articleId = wikiObjectParser.GetWikiObjectIdentifier("") as AWikiArticleIdentifier;

            var articleProvider = new WikiArticleProvider();
            var html = await articleProvider.GetArticleHtmlAsync(articleId);

            var htmlProcessor = new HtmlProcessor();
            var article = await htmlProcessor.ProcessArticleAsync(html);

            var imageGenerator = new WikiImageGenerator();
            imageGenerator.CreateArticleImage(article, null);
        }
    }
}
