using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Wikify.Parsing.MwParser;

namespace Wikify.Test.Parsing
{
    [TestClass]
    public class MwParserApiTest
    {
        private static MwParserApi _mwParserApi;
        private static ILoggerFactory _loggerFactory;
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {

            _loggerFactory = new LoggerFactory();
            _mwParserApi = new MwParserApi(_loggerFactory.CreateLogger<MwParserApi>());
        }


        [TestMethod]
        public async Task TestGetArticleMwRootAsync()
        {

        }

        [TestMethod]
        public void TestGetArticleContainer()
        {

        }
    }
}
