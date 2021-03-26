using Wikify.Common.Content.Types;
using Wikify.Common.Id;

namespace Wikify.Parsing.Id
{
    internal interface IIdParser<T> where T : AWikiContent
    {
        public IIdentifier<T> GetObjectIdentifier(string input);
    }
}
