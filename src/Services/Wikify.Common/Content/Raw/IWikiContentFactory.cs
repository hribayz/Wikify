using System.Drawing;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content.Raw
{
    /// <summary>
    /// Provides methods for creating instances of <see cref="IWikiContent"/> descendants (e.g. <see cref="IWikiImage"/>, <see cref="IWikiArticle"/>).
    /// Ensures the <see cref="IIdentifier"/>, the <see cref="ILicense"/> and the content references are set at creation.
    /// </summary>
    public interface IWikiContentFactory
    {
        public IWikiArticle CreateWikiArticle(IArticleIdentifier identifier, ILicense license, string content, ContentModel contentModel);
        public IWikiImage CreateWikiImage(IImageIdentifier identifier, ILicense license, Image image);
        public IWikiData CreateWikiData(string payload, ContentModel contentModel);
    }
}
