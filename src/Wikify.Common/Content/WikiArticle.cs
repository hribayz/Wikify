using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    public class WikiArticle : IWikiArticle
    {
        public string ArticleData { get; private set; }
        public IArticleIdentifier Identifier { get; private set; }
        public ILicense License { get; private set; }
        public WikiContentModel ContentModel { get; private set; }

        /// <summary>
        /// Do not instantiate directly. Use DI.
        /// </summary>
        internal WikiArticle(IArticleIdentifier identifier, ILicense license, string articleData, WikiContentModel contentModel)
        {
            Identifier = identifier;
            License = license;
            ArticleData = articleData;
            ContentModel = contentModel;
        }
    }
}
