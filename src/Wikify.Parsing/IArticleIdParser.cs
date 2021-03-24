using Wikify.Common;

namespace Wikify.Parsing
{
    internal interface IArticleIdParser : IObjectIdParser
    {
        public AArticleIdentifier GetWikiArticleIdentifier(string input);
    }
}
