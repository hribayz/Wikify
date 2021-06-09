using Wikify.Common.Id;

namespace Wikify.Common.Content.Raw
{
    /// <summary>
    /// A container that provides data members for articles.
    /// </summary>
    public interface IWikiArticle : IWikiContent
    {
        public IArticleIdentifier Identifier { get; }
        public string ArticleData { get; }
        public ContentModel ContentModel { get; }
    }
}
