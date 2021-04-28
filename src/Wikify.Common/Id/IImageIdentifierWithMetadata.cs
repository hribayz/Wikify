using System.Collections.Immutable;

namespace Wikify.Common.Id
{
    public interface IImageIdentifierWithMetadata : IImageIdentifier
    {
        public IImmutableDictionary<string, string> ImageMetadata { get; }
    }
}
