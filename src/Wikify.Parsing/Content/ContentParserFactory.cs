using Wikify.Common.Content.Types;

namespace Wikify.Parsing.Content
{
    public class ContentParserFactory : IContentParserFactory
    {
        public IStreamedContentParser<T> CreateStreamedContentParser<T>() where T : AWikiContent
        {
            throw new System.NotImplementedException();
        }
    }
}
