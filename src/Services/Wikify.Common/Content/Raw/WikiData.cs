namespace Wikify.Common.Content.Raw
{
    public class WikiData : IWikiData
    {
        public WikiData(string payload, ContentModel contentModel)
        {
            Payload = payload;
            ContentModel = contentModel;
        }

        public string Payload { get; private set; }
        public ContentModel ContentModel { get; private set; }

    }
}
