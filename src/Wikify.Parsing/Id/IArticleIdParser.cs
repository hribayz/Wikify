using Wikify.Common.Id;

namespace Wikify.Parsing.Id
{
    internal interface IArticleIdParser : IObjectIdParser
    {
        public  GetWikiArticleIdentifier(string input);
    }
}
