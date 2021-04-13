using System.Drawing;

namespace Wikify.Common.Content
{
    public interface IWikiArticle : IWikiMedia
    {
        public string ArticleData { get; }
        public WikiContentModel ContentModel { get; }
    }
}
