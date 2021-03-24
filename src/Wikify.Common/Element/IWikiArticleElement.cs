using System.Collections.Generic;
using Wikify.Common.License;

namespace Wikify.Common.Element
{
    public interface IWikiArticleElement
    {
        public string GetSource();
        public IWikiObjectLicense GetLicense();
        public IEnumerable<IWikiArticleElement> GetChildren();
    }
}
