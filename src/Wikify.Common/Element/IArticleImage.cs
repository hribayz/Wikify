using System.Drawing;

namespace Wikify.Common.Element
{
    public interface IArticleImage : IArticleElement
    {
        public Image GetImage();
    }
}
