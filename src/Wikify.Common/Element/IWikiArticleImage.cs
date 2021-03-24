using System.Drawing;

namespace Wikify.Common.Element
{
    public interface IWikiArticleImage : IWikiArticleElement
    {
        public Image GetImage();
    }
}
