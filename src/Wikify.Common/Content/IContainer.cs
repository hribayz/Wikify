using Wikify.Common.Content.Types;

namespace Wikify.Common.Content
{
    public interface IContainer<T> : IElement where T : AWikiContent
    {
        public T GetContent();
    }

}
