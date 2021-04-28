namespace Wikify.Common.Id
{
    public class ImageIdentifierFactory : IImageIdentifierFactory
    {
        public IImageIdentifier GetIDentifier(string fileName, string creditUri, string imageUri)
        {
            return new ImageIdentifier(fileName, creditUri, imageUri);
        }

    }
}
