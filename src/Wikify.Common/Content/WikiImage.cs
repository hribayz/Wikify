using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    public class WikiImage : IWikiImage
    {
        public Image Image { get; private set; }
        public IImageIdentifier Identifier { get; private set; }
        public ILicense License { get; private set; }


        public WikiImage(IImageIdentifier identifier, ILicense license, Image image)
        {
            Identifier = identifier;
            License = license;
            Image = image;
        }
    }
}
