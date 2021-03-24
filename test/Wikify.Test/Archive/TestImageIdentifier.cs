using Wikify.Common;

namespace Wikify.Test.Archive
{
    internal class TestImageIdentifier : IImageIdentifier
    {
        private string _url;
        public TestImageIdentifier(string url)
        {
            _url = url;
        }

        public string GetUrl() => _url;
    }
}
