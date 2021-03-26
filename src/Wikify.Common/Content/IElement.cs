using System.Collections.Generic;
using Wikify.Common.Content.Types;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    public interface IElement
    {
        public IIdentifier GetIdentifier();
        public ILicense GetLicense();
        public IEnumerable<IElement> GetChildren();
    }
    public interface IElement<T> : IElement where T : AWikiContent
    {
        public T GetContent();
    }
}
