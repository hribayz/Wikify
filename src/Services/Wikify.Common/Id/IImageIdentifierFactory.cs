using System.Collections.Generic;

namespace Wikify.Common.Id
{
    public interface IImageIdentifierFactory
    {
        public IImageIdentifier CreateIdentifier(string title, string creditUri, string imageUri, IReadOnlyDictionary<string, string> ImageMetadata);
    }
}
