using System.Collections.Generic;
using Wikify.Common.License;

namespace Wikify.Common.Element
{
    public interface IArticleElement
    {
        public string GetSource();
        public IObjectLicense GetLicense();
        public IEnumerable<IArticleElement> GetChildren();
    }
}
