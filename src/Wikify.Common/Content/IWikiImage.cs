using System.Drawing;

namespace Wikify.Common.Content
{
    public interface IWikiImage : IWikiMedia
    {
        public Image Image { get; }
    }
}
