using System.Drawing;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    public class WikiMediaFactory : IWikiMediaFactory
    {
        public IWikiArticle CreateWikiArticle(IIdentifier identifier, ILicense license, string content)
        {
            return new WikiArticle(identifier, license, content);
        }

        public IWikiImage CreateWikiImage(IIdentifier identifier, ILicense license, Image image)
        {
            return new WikiImage(identifier, license, image);
        }
    }
}
