using Wikify.Common;
using System.Drawing;
using System.Threading.Tasks;

namespace Wikify.Archive
{
    public interface IImageArchive
    {
        Task<Image> GetImageAsync(AImageIdentifier imageIdentifier);
    }
    public class WikiImageProvider : IImageArchive
    {
        public Task<Image> GetImageAsync(AImageIdentifier imageIdentifier)
        {
            throw new System.NotImplementedException();
        }
    }
}