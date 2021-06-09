using MwParserFromScratch.Nodes;
using System.Collections.Generic;
using System.Linq;
using Wikify.Common.Content.Parsed;
using Wikify.Common.Content.Raw;

namespace Wikify.Parser.Content
{

    public class ArticleContainer : WikiComponent, IWikiContainer<IWikiArticle>
    {
        public IWikiArticle Content { get; private set; }
        public ArticleContainer(IWikiData rawData, IWikiArticle wikiArticle, Node startNode, Node endNode) : base(rawData, WikiComponentType.Article, startNode, endNode)
        {
            Content = wikiArticle;
        }
    }
}
