using System.Drawing;
using System.IO;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content.Raw
{
    /// <inheritdoc cref="IWikiContentFactory"/>
    public class WikiContentFactory : IWikiContentFactory
    {
        public IWikiArticle CreateWikiArticle(IArticleIdentifier identifier, ILicense license, string content, ContentModel contentModel)
        {
            return new WikiArticle(identifier, license, content, contentModel);
        }

        public IWikiImage CreateWikiImage(IImageIdentifier identifier, ILicense license, Image image)
        {
            return new WikiImage(identifier, license, image);
        }

        public IWikiData CreateWikiData(string payload, ContentModel contentModel)
        {
            return new WikiData(payload, contentModel);
        }

    }
}
