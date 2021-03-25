using System.Collections.Generic;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    public interface IElement
    {
        public IElementIdentifier GetIdentifier();
        public ILicense GetLicense();
        public IEnumerable<IElement> GetChildren();
    }
}
