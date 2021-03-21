using Wikify.Common;

namespace Wikify.Archive
{
    public interface IWikiImageProvider
    {
        public System.Drawing.Image GetImage(AWikiImageIdentifier imageIdentifier);
    }
}