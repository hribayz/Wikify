using System;
using Wikify.Common;

namespace Wikify.Parsing
{
    internal interface IWikiObjectParser
    {
        public AWikiObjectIdentifier GetWikiObjectIdentifier(string input);
    }
    public class WikiObjectParser : IWikiObjectParser
    {
        public AWikiObjectIdentifier GetWikiObjectIdentifier(string input)
        {
            throw new NotImplementedException();
        }
    }
}
