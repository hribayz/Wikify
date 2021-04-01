using System.Collections.Generic;
using Wikify.Common.Content;
using Wikify.Common.Content.Types;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Archive.AngleSharp
{
    public class ImageContainer : IContainer<WikiImage>, IIdentifiable<WikiImage>
    {
        public IEnumerable<IElement> GetChildren()
        {
            throw new System.NotImplementedException();
        }

        public WikiImage GetContent()
        {
            throw new System.NotImplementedException();
        }

        public string GetHtml()
        {
            throw new System.NotImplementedException();
        }

        public ILicense GetLicense()
        {
            throw new System.NotImplementedException();
        }

        public IIdentifier<WikiImage> GetIdentifier()
        {
            throw new System.NotImplementedException();
        }
    }
}
