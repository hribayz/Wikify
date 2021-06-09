using MwParserFromScratch.Nodes;
using System.Collections.Generic;
using Wikify.Common.Content.Parsed;
using Wikify.Common.Content.Raw;

namespace Wikify.Parser.Content
{
    public class ImageContainer : WikiComponent, IWikiContainer<IWikiImage>
    {
        public IWikiImage Content { get; private set; }

        public ImageContainer(IWikiData rawData, IWikiImage wikiImage, WikiComponentType componentType, Node startNode, Node endNode) : base(rawData, componentType, startNode, endNode)
        {
            Content = wikiImage;
        }
    }
}
