using System.Collections.Generic;

namespace Wikify.Common.Id
{
    public interface IImageIdentifier : IIdentifier
    {
        public string CreditUri { get; }
        public string ImageUri { get; }
        public IReadOnlyDictionary<string, string> ImageMetadata { get; }
    }
}
