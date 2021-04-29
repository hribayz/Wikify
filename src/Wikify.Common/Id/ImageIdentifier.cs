using System.Collections.Generic;

namespace Wikify.Common.Id
{
    public class ImageIdentifier : IImageIdentifier, IImageIdentifierWithMetadata
    {
        private readonly IReadOnlyDictionary<string, string>? _imageMetadata;
        public string Title { get; }
        public string CreditUri { get; }
        public string ImageUri { get; }

        public IReadOnlyDictionary<string, string> ImageMetadata 
        {
            get 
            {
                if (_imageMetadata == null)
                {
                    throw new System.NotSupportedException();
                }
                return _imageMetadata; 
            }
        }

        public ImageIdentifier(string title, string creditUri, string imageUri)
        {
            Title = title;
            CreditUri = creditUri;
            ImageUri = imageUri;
        }

        public ImageIdentifier(string title, string creditUri, string imageUri, IReadOnlyDictionary<string, string> imageMetadata)
        {
            Title = title;
            CreditUri = creditUri;
            ImageUri = imageUri;
            _imageMetadata = imageMetadata;
        }
    }
}
