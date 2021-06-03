using Wikify.Common.Id;

namespace Wikify.Common.Content
{
    /// <summary>
    /// A container that provides data members for articles.
    /// </summary>
    public interface IWikiArticle : IWikiMedia
    {
        public IArticleIdentifier Identifier { get; }
        public string ArticleData { get; }
        public TextContentModel ContentModel { get; }
    }
}
