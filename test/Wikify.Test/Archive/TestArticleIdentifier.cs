using Wikify.Common;
using Wikify.Common.Content.Types;
using Wikify.Common.Id;

namespace Wikify.Test.Archive
{
    internal class TestArticleIdentifier : IIdentifier<WikiArticle>
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
