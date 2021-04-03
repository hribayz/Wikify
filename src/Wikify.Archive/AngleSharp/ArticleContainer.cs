using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Archive.AngleSharp
{
    public class ArticleContainer : IWikiContainer<WikiArticle>, IIdentifiable
    {
        private IDocument _document;
        private List<IWikiComponent> _children;
        private bool _extracted;

        public IContentIdentifier ContentIdentifier => throw new NotImplementedException();

        // raise event when parsing done.
        public ArticleContainer(IDocument document)
        {
            _document = document;
            _children = new List<IWikiComponent>();
        }
        private async Task<IWikiComponent> ExtractChildrenAsync(IElement element)
        {
            // recursively evaluate elements

            

            foreach (var child in element.Children)
            {
                child.
            }
            ;


        }
        public IEnumerable<Common.Content.IWikiComponent> GetChildren()
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

    }
}
