using Wikify.Common;
using Wikify.Common.Content.Types;
using Wikify.Common.Id;

namespace Wikify.Test.Archive
{
    internal class TestImageIdentifier : IIdentifier<WikiImage>
    {
        private string _url;
        public TestImageIdentifier(string url)
        {
            _url = url;
        }

        public string GetUrl() => _url;
    }
}
