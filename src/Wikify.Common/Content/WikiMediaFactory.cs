using System.Drawing;
using System.IO;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    /// <inheritdoc cref="IWikiMediaFactory"/>
    public class WikiMediaFactory : IWikiMediaFactory
    {
        public IWikiArticle CreateWikiArticle(IArticleIdentifier identifier, ILicense license, string content, TextContentModel contentModel)
        {
            return new WikiArticle(identifier, license, content, contentModel);
        }

        public IWikiImage CreateWikiImage(IImageIdentifier identifier, ILicense license, Image image)
        {
            return new WikiImage(identifier, license, image);
        }
    }
}
