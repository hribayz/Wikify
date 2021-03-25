using System.Collections.Generic;
using Wikify.Common.Content.Types;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    public class ImageElement : IElement<ImageContent>
    {
        public IEnumerable<IElement> GetChildren()
        {
            throw new System.NotImplementedException();
        }

        public ImageContent GetContent()
        {
            throw new System.NotImplementedException();
        }

        public IElementIdentifier GetIdentifier()
        {
            throw new System.NotImplementedException();
        }

        public ILicense GetLicense()
        {
            throw new System.NotImplementedException();
        }
    }
}
