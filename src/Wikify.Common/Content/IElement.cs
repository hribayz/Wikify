using System.Collections.Generic;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    public interface IElement
    {
        public ILicense GetLicense();
        public IEnumerable<IElement> GetChildren();
        public string GetHtml();
    }
}
