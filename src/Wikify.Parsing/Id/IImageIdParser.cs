using System.Threading.Tasks;
using Wikify.Common.Id;

namespace Wikify.Parsing.Id
{
    public interface IImageIdParser
    {
        public Task<IImageIdentifier> GetIdentifierAsync(string imageTitle);
        public Task<IImageIdentifierWithMetadata> GetIdentifierWithMetadataAsync(string imageTitle);
    }
}
