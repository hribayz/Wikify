namespace Wikify.Common.Id
{
    public interface IImageIdentifier : IIdentifier
    {
        public string CreditUri { get; }
        public string ImageUri { get; }
    }
}
