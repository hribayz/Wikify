using System.Drawing;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    public class WikiImage : IWikiImage
    {
        public Image Image { get; private set; }
        public IIdentifier Identifier { get; private set; }
        public ILicense License { get; private set; }

        public WikiImage(IIdentifier identifier, ILicense license, Image image)
        {
            Image = image;
            Identifier = identifier;
            License = license;
        }
    }
}
