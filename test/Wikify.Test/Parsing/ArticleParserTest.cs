using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Parsing.Content;

namespace Wikify.Test.Parsing
{
    [TestClass]
    public class ArticleParserTest
    {
        private static LoggerFactory _loggerFactory;
        private static IArticleParser _articleParser;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context, IArticleParser articleParser)
        {
            _loggerFactory = new LoggerFactory();
        }

        [TestMethod]
        [DataRow("Edinburgh - Wikipedia.html")]
        public async Task TestParseArticle(string fileLocation)
        {
            var articleHtml = File.ReadAllText(fileLocation);
            _articleParser.LoadArticleAsync()
        }

    }
}
