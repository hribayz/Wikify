namespace Wikify.Common.Content.Raw
{
    /// <summary>
    /// An immutable container for storing the original content payload.
    /// </summary>
    public interface IWikiData
    {
        /// <summary>
        /// Gets the payload in unmodified form.
        /// </summary>
        public string Payload { get; }

        /// <summary>
        /// Gets the content model of the payload.
        /// </summary>
        public ContentModel ContentModel { get; }
        public IWikiData EncodeModifiedPayload(string modifiedPayload);
    }
}
