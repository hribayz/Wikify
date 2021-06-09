using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content.Raw
{
    /// <inheritdoc cref="IWikiArticle"/>
    public class WikiArticle : WikiContentBase, IWikiArticle
    {
        public string ArticleData { get; private set; }
        public IArticleIdentifier Identifier { get; private set; }
        public ContentModel ContentModel { get; private set; }

        internal WikiArticle(IArticleIdentifier identifier, ILicense license, string articleData, ContentModel contentModel) : base(license)
        {
            Identifier = identifier;
            ArticleData = articleData;
            ContentModel = contentModel;
        }
    }
}
