using System.Collections.Generic;
using Wikify.Common.Content.Types;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    public class ImageElement : IElement<WikiImage>
    {
        public IEnumerable<IElement> GetChildren()
        {
            throw new System.NotImplementedException();
        }

        public WikiImage GetContent()
        {
            throw new System.NotImplementedException();
        }

        public IIdentifier GetIdentifier()
        {
            throw new System.NotImplementedException();
        }

        public ILicense GetLicense()
        {
            throw new System.NotImplementedException();
        }
    }
}
