using MwParserFromScratch.Nodes;
using System.Collections.Generic;
using System.Linq;
using Wikify.Common.Content;

namespace Wikify.Parsing.Content
{

    public class ArticleContainer : WikiComponent, IWikiContainer<IWikiArticle>
    {
        public IWikiArticle Content { get; private set; }
        public ArticleContainer(IWikiArticle wikiArticle, IReadOnlyCollection<IWikiComponent> children, Node startNode, Node endNode) : base(WikiComponentType.Article, children, startNode, endNode)
        {
            Content = wikiArticle;
        }
    }
}
