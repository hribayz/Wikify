namespace Wikify.Common.Id
{
    public class ImageIdentifier : IImageIdentifier
    {
        public string Title { get; }
        public string CreditUri { get; }
        public string ImageUri { get; }

        public ImageIdentifier(string fileName, string creditUri, string imageUri)
        {
            Title = fileName;
            CreditUri = creditUri;
            ImageUri = imageUri;
        }

    }
}
