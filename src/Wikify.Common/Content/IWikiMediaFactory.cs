using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    public interface IWikiMediaFactory
    {
        public IWikiArticle CreateWikiArticle(IIdentifier identifier, ILicense license, string content);
        public IWikiImage CreateWikiImage(IIdentifier identifier, ILicense license, Image image);
    }
}
