﻿using AngleSharp.Dom;
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
        private IDocument _document;
        private List<IComponent> _children;
        private bool _extracted;

        // raise event when parsing done.
        public ArticleContainer(IDocument document)
        {
            _document = document;
            _children = new List<IComponent>();
        }
        private async Task<IComponent> ExtractChildrenAsync(IElement element)
        {
            // recursively evaluate elements

            foreach (var child in element.GetDescendants())
            {
                child.b
            }
            ;


        }
        public IEnumerable<Common.Content.IComponent> GetChildren()
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
