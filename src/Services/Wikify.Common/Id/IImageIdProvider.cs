using System.Collections.Generic;
using System.Threading.Tasks;
using Wikify.Common.Id;

namespace Wikify.Common.Id
{ 
    public interface IImageIdProvider
    {
        public Task<IImageIdentifier> GetIdentifierAsync(string imageTitle);
        public Task<IReadOnlyDictionary<string, IImageIdentifier>> GetIdentifiersAsync(IEnumerable<string> imageTitles);
    }
}
