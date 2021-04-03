namespace Wikify.Common.Content
{
    public interface IWikiContainer<T> : IWikiComponent where T : IWikiMedia
    {
        public T Content { get; }
    }

}
