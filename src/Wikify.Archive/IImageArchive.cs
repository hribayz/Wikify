using Wikify.Common;
using System.Drawing;
using System.Threading.Tasks;

namespace Wikify.Archive
{
    public interface IImageArchive
    {
        Task<Image> GetImageAsync(IImageIdentifier imageIdentifier);
    }
    public class WikiImageProvider : IImageArchive
    {
        public Task<Image> GetImageAsync(IImageIdentifier imageIdentifier)
        {
            throw new System.NotImplementedException();
        }
    }
}