using Wikify.Common;

namespace Wikify.Test.Archive
{
    internal class TestObjectIdentifier : IObjectIdentifier
    {
        private string _url;
        public TestObjectIdentifier(string url)
        {
            _url = url;
        }
        public string GetUrl() => _url;
    }
}
