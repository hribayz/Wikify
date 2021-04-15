using Wikify.Common.Id;

namespace Wikify.Common.Content
{
    public interface IWikiArticle : IWikiMedia
    {
        public IArticleIdentifier Identifier { get; }
        public string ArticleData { get; }
        public WikiContentModel ContentModel { get; }
    }
}
