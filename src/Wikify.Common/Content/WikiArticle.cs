using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    /// <inheritdoc cref="IWikiArticle"/>
    public class WikiArticle : IWikiArticle
    {
        public string ArticleData { get; private set; }
        public IArticleIdentifier Identifier { get; private set; }
        public ILicense License { get; private set; }
        public TextContentModel ContentModel { get; private set; }

        internal WikiArticle(IArticleIdentifier identifier, ILicense license, string articleData, TextContentModel contentModel)
        {
            Identifier = identifier;
            License = license;
            ArticleData = articleData;
            ContentModel = contentModel;
        }
    }
}
