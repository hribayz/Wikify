using System.Drawing;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    public interface IWikiArticle : IWikiMedia
    {
        public string ArticleHtml { get; }

    }
}
