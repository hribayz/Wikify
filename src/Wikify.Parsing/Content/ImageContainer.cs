using System.Collections.Generic;
using Wikify.Common.Content;

namespace Wikify.Parsing.Content
{
    public class ImageContainer : IWikiContainer<IWikiImage>
    {
        public IWikiImage Content => throw new System.NotImplementedException();

        public IEnumerable<IWikiComponent> Children => throw new System.NotImplementedException();

        public WikiComponentType ComponentType => throw new System.NotImplementedException();
    }
}
