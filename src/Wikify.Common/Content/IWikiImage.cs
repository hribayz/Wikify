using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Wikify.Common.Id;

namespace Wikify.Common.Content
{
    public interface IWikiImage : IWikiMedia
    {
        public Image Image { get; }
        public IImageIdentifier Identifier { get; }
    }
}
