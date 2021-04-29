using System.Collections.Generic;

namespace Wikify.Common.Id
{
    public interface IImageIdentifierWithMetadata : IImageIdentifier
    {
        public IReadOnlyDictionary<string, string> ImageMetadata { get; }
    }
}
