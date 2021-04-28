namespace Wikify.Common.Id
{
    public interface IImageIdentifierFactory
    {
        public IImageIdentifier GetIDentifier(string fileName, string creditUri, string imageUri);
    }
}
