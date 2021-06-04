using System.Collections.Generic;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Id;

namespace Wikify.Archive
{
    public interface IImageArchive
    {
        public Task<IWikiImage> GetImageAsync(IImageIdentifier imageIdentifier);
        public Task<IReadOnlyCollection<IWikiImage>> GetImagesAsync(IEnumerable<IImageIdentifier> imageIdentifiers);
    }
}