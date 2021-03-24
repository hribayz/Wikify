using Wikify.Common;

namespace Wikify.Parsing
{
    internal interface IObjectIdParser
    {
        public IObjectIdentifier GetObjectIdentifier(string input);
    }
}
