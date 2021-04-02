using System.Collections.Generic;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    public interface IComponent
    {
        public ILicense GetLicense();
        public IEnumerable<IComponent> GetChildren();
        public string GetHtml();
    }
}
