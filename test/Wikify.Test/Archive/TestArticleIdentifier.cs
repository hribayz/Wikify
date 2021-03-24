using Wikify.Common;

namespace Wikify.Test.Archive
{
    internal class TestArticleIdentifier : IArticleIdentifier
    {
        private string _url;
        private string _title;
        public TestArticleIdentifier(string url, string title)
        {
            _url = url;
            _title = title;
        }

        public string GetTitle() => _title;
        public string GetUrl() => _url;
    }
}
