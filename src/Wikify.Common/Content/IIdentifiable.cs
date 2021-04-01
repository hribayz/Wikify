using Wikify.Common.Content.Types;
using Wikify.Common.Id;

namespace Wikify.Common.Content
{
    public interface IIdentifiable<T> where T : AWikiContent
    {
        public IIdentifier<T> GetIdentifier();
    }

}
