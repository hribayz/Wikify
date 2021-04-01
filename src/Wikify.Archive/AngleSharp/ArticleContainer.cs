using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Content.Types;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Archive.AngleSharp
{
    public class ArticleContainer : IContainer<WikiArticle>, IIdentifiable<WikiArticle>
    {
        public ArticleContainer(IDocument document)
        {
            ;
        }
        public IEnumerable<Common.Content.IElement> GetChildren()
        {
            throw new NotImplementedException();
        }

        public WikiArticle GetContent()
        {
            throw new NotImplementedException();
        }

        public string GetHtml()
        {
            throw new NotImplementedException();
        }


        public ILicense GetLicense()
        {
            throw new NotImplementedException();
        }

        public IIdentifier<WikiArticle> GetIdentifier()
        {
            throw new NotImplementedException();
        }
    }
}
