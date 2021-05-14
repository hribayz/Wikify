using MwParserFromScratch.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content;

namespace Wikify.Parsing.Content
{
    internal interface IWikiContentFactory
    {
        public IWikiComponent CreateComponent(WikiComponentType componentType, IReadOnlyCollection<IWikiComponent> children, Node startNode, Node endNode);
        public IWikiContainer<IWikiArticle> CreateArticle(IWikiArticle article, IReadOnlyCollection<IWikiComponent> children, Node startNode, Node endNode);
        public IWikiContainer<IWikiImage> CreateImage(IWikiImage image, WikiComponentType componentType, IReadOnlyCollection<IWikiComponent> children, Node startNode, Node endNode);
    }
    public class WikiContentFactory : IWikiContentFactory
    {
        public IWikiContainer<IWikiArticle> CreateArticle(IWikiArticle article, IReadOnlyCollection<IWikiComponent> children, Node startNode, Node endNode)
        {
            return new ArticleContainer(article, children, startNode, endNode);
        }

        public IWikiComponent CreateComponent(WikiComponentType componentType, IReadOnlyCollection<IWikiComponent> children, Node startNode, Node endNode)
        {
            return new WikiComponent(componentType, children, startNode, endNode);
        }

        public IWikiContainer<IWikiImage> CreateImage(IWikiImage image, WikiComponentType componentType, IReadOnlyCollection<IWikiComponent> children, Node startNode, Node endNode)
        {
            return new ImageContainer(image, componentType, children, startNode, endNode);
        }
    }
}
