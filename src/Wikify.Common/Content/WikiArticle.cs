using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    public class WikiArticle : IWikiArticle
    {
        public string ArticleData { get; private set; }
        public IIdentifier Identifier { get; private set; }
        public ILicense License { get; private set; }

        /// <summary>
        /// Do not instantiate directly. Use DI.
        /// </summary>
        internal WikiArticle(IIdentifier identifier, ILicense license, string articleData)
        {
            Identifier = identifier;
            License = license;
            ArticleData = articleData;
        }
    }
}
