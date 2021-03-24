using Wikify.Common;

namespace Wikify.Parsing
{
    internal interface IWikiArticleIdParser : IWikiObjectIdParser
    {
        public AWikiArticleIdentifier GetWikiArticleIdentifier(string input);
    }
}
