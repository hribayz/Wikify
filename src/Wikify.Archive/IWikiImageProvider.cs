using Wikify.Common;
using System.Drawing;

namespace Wikify.Archive
{
    interface IWikiImageArchive
    {
        Image GetImage(AWikiImageIdentifier imageIdentifier);
    }
    public class WikiImageProvider : IWikiImageArchive
    {
        public Image GetImage(AWikiImageIdentifier imageIdentifier)
        {
            throw new System.NotImplementedException();
        }
    }
}