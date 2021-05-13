using Wikify.Common.Id;
using Wikify.Common.License;
using Newtonsoft.Json;

namespace Wikify.Common.Content
{
    /// <inheritdoc cref="IWikiArticle"/>
    public class WikiArticle : WikiMediaBase, IWikiArticle
    {
        public string ArticleData { get; private set; }
        public IArticleIdentifier Identifier { get; private set; }
        public TextContentModel ContentModel { get; private set; }

        internal WikiArticle(IArticleIdentifier identifier, ILicense license, string articleData, TextContentModel contentModel) : base(license)
        {
            Identifier = identifier;
            ArticleData = articleData;
            ContentModel = contentModel;
        }
    }
}
