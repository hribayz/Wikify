using System.Collections.Generic;

namespace Wikify.Common.Id
{
    public interface IImageIdentifierFactory
    {
        public IImageIdentifier GetIdentifier(string title, string creditUri, string imageUri);
        public IImageIdentifierWithMetadata GetIdentifier(string title, string creditUri, string imageUri, IReadOnlyDictionary<string, string> ImageMetadata);
        public IImageIdentifierWithMetadata GetIdentifier(IImageIdentifier imageIdentifier, IReadOnlyDictionary<string, string> ImageMetadata);
    }
}
