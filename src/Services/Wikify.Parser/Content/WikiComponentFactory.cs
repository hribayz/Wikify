using MwParserFromScratch.Nodes;
using Wikify.Common.Content.Parsed;
using Wikify.Common.Content.Raw;

namespace Wikify.Parser.Content
{
    public class WikiComponentFactory : IWikiComponentFactory
    {
        public ArticleContainer CreateArticle(IWikiData rawData, IWikiArticle article, Node startNode, Node endNode)
        {
            return new ArticleContainer(rawData, article, startNode, endNode);
        }

        public WikiComponent CreateComponent(IWikiData rawData, WikiComponentType componentType, Node startNode, Node endNode)
        {
            return new WikiComponent(rawData, componentType, startNode, endNode);
        }

        public ImageContainer CreateImage(IWikiData rawData, IWikiImage image, WikiComponentType componentType, Node startNode, Node endNode)
        {
            return new ImageContainer(rawData, image, componentType, startNode, endNode);
        }
    }
}
