using MwParserFromScratch.Nodes;
using System.Collections.Generic;
using Wikify.Common.Content;

namespace Wikify.Parsing.Content
{
    public class ImageContainer : WikiComponent, IWikiContainer<IWikiImage>
    {
        public IWikiImage Content { get; private set; }

        public ImageContainer(IWikiImage wikiImage, WikiComponentType componentType, Node startNode, Node endNode) : base(componentType, startNode, endNode)
        {
            Content = wikiImage;
        }
    }
}
