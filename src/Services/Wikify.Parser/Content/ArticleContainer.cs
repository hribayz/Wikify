using MwParserFromScratch.Nodes;
using System.Collections.Generic;
using System.Linq;
using Wikify.Common.Content;

namespace Wikify.Parser.Content
{

    public class ArticleContainer : WikiComponent, IWikiContainer<IWikiArticle>
    {
        public IWikiArticle Content { get; private set; }
        public ArticleContainer(IWikiArticle wikiArticle, Node startNode, Node endNode) : base(WikiComponentType.Article, startNode, endNode)
        {
            Content = wikiArticle;
        }
    }
}
