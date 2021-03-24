using Wikify.Common;

namespace Wikify.Parsing
{
    internal interface IArticleIdParser : IObjectIdParser
    {
        public IArticleIdentifier GetWikiArticleIdentifier(string input);
    }
}
