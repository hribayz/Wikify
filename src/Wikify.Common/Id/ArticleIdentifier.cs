using Wikify.Common;
using Wikify.Common.Content.Types;
using Wikify.Common.Id;

namespace Wikify.Common.Id
{
    public class ArticleIdentifier : IIdentifier<WikiArticle>
    {
        private string _url;
        private string _title;
        public ArticleIdentifier(string url)
        {
            _url = url;
        }

        public string GetFileName()
        {
            throw new System.NotImplementedException();
        }

        public string GetTitle() => _title;
        public string GetUrl() => _url;
    }
}
