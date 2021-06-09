using System.Drawing;
using Wikify.Common.Id;

namespace Wikify.Common.Content.Raw
{
    /// <summary>
    /// A container that provides data members for images.
    /// </summary>
    public interface IWikiImage : IWikiContent
    {
        public Image Image { get; }
        public IImageIdentifier Identifier { get; }
    }
}
