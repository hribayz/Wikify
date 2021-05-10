using System.Collections.Generic;
using Wikify.Common.Content;

namespace Wikify.Parsing.Content
{
    public class ImageContainer : WikiComponent, IWikiContainer<IWikiImage>
    {
        public IWikiImage Content { get; private set; }

        public ImageContainer(IWikiImage wikiImage, WikiComponentType componentType, IReadOnlyCollection<IWikiComponent> children) : base(componentType, children)
        {
            Content = wikiImage;
        }
    }
}
