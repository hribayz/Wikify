using MwParserFromScratch.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content;

namespace Wikify.Parsing.Content
{
    /// <summary>
    /// Contains methods for creating <see cref="WikiComponent"/> descendants from <see cref="MwParserFromScratch"/> AST nodes.
    /// </summary>
    public interface IWikiContentFactory
    {
        public WikiComponent CreateComponent(WikiComponentType componentType, Node startNode, Node endNode);
        public ArticleContainer CreateArticle(IWikiArticle article, Node startNode, Node endNode);
        public ImageContainer CreateImage(IWikiImage image, WikiComponentType componentType, Node startNode, Node endNode);
    }
}
