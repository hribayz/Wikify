namespace Wikify.Common.Content.Raw
{
    public interface IWikiData
    {
        public string Payload { get; }
        public ContentModel ContentModel { get; }
    }
}
