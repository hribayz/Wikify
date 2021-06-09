using Wikify.Common.Content.Raw;

namespace Wikify.Common.Content.Parsed
{
    /// <summary>
    /// An <see cref="IWikiComponent"/> that has a content as defined by <see cref="IWikiContent"/>.
    /// </summary>
    public interface IWikiContainer<T> : IWikiComponent where T : IWikiContent
    {
        public T Content { get; }
    }
}
