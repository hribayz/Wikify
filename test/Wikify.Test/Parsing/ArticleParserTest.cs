using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wikify.Parsing.Content;

namespace Wikify.Test.Parsing
{
    [TestClass]
    public class ArticleParserTest
    {
        private static ArticleParser _articleParser;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _articleParser = new ArticleParser();
        }

        [TestMethod]
        [DataRow("C:\\Users\\TK\\Desktop\\Laboratori Nazionali del Gran Sasso - Wikipedia.html")]
        public async Task TestParseArticle(string articleFileName)
        {
            var html = File.ReadAllText(articleFileName);
            await _articleParser.ParseElementAsync(html);
            ;
        }

    }
}
