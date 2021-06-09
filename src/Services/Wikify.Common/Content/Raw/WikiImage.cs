using System.Drawing;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content.Raw
{
    /// <inheritdoc cref="IWikiImage"/>
    public class WikiImage : WikiContentBase, IWikiImage
    {
        public Image Image { get; private set; }
        public IImageIdentifier Identifier { get; private set; }

        public WikiImage(IImageIdentifier identifier, ILicense license, Image image) : base(license)
        {
            Identifier = identifier;
            Image = image;
        }
    }
}
