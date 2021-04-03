using System.Drawing;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    public interface IWikiImage : IWikiMedia
    {
        public Image Image { get; }
    }
}
