using Wikify.Common.Content.Types;

namespace Wikify.Common.Content
{
    public interface IContainer<T> : IComponent where T : AWikiContent
    {
        public T GetContent();
    }

}
