using Wikify.Common;

namespace Wikify.Parsing
{
    internal interface IWikiObjectIdParser
    {
        public AWikiObjectIdentifier GetWikiObjectIdentifier(string input);
    }
}
