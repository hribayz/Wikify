using System.Collections.Generic;

namespace Wikify.Common.Id
{
    public class ImageIdentifierFactory : IImageIdentifierFactory
    {

        public IImageIdentifier CreateIdentifier(string title, string creditUri, string imageUri, IReadOnlyDictionary<string, string> imageMetadata)
        {
            return new ImageIdentifier(title, creditUri, imageUri, imageMetadata);
        }

    }
}
