using Wikify.Common;

namespace Wikify.Parsing
{
    internal interface IObjectIdParser
    {
        public AObjectIdentifier GetObjectIdentifier(string input);
    }
}
