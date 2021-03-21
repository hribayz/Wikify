using Wikify.Common;
using System.Drawing;

namespace Wikify.Archive
{
    interface IWikiImageProvider
    {
        Image GetImage(AWikiImageIdentifier imageIdentifier);
    }
}