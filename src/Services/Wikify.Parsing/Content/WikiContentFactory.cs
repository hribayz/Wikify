using MwParserFromScratch.Nodes;
using Wikify.Common.Content;

namespace Wikify.Parsing.Content
{
    public class WikiContentFactory : IWikiContentFactory
    {
        public ArticleContainer CreateArticle(IWikiArticle article, Node startNode, Node endNode)
        {
            return new ArticleContainer(article, startNode, endNode);
        }

        public WikiComponent CreateComponent(WikiComponentType componentType, Node startNode, Node endNode)
        {
            return new WikiComponent(componentType, startNode, endNode);
        }

        public ImageContainer CreateImage(IWikiImage image, WikiComponentType componentType, Node startNode, Node endNode)
        {
            return new ImageContainer(image, componentType, startNode, endNode);
        }
    }
}
