using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Wikify.Parsing.MwParser;

namespace Wikify.Test.Parsing
{
    [TestClass]
    public class PatternMatchingServiceTests : WikifyTestBase
    {
        public async Task TestTemplateHasNameAsync(string title)
        {
            var patternMatchingService = GetService<IPatternMatchingService>();

        }
    }
}
