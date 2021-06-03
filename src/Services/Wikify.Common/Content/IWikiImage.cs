using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Wikify.Common.Id;

namespace Wikify.Common.Content
{
    /// <summary>
    /// A container that provides data members for images.
    /// </summary>
    public interface IWikiImage : IWikiMedia
    {
        public Image Image { get; }
        public IImageIdentifier Identifier { get; }
    }
}
