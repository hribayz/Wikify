namespace Wikify.Common.Content
{
    /// <summary>
    /// An <see cref="IWikiComponent"/> that has a content as defined by <see cref="IWikiMedia"/>.
    /// </summary>
    public interface IWikiContainer<T> : IWikiComponent where T : IWikiMedia
    {
        public T Content { get; }
    }
}
