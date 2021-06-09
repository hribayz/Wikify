using MwParserFromScratch.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content.Parsed;
using Wikify.Common.Content.Raw;

namespace Wikify.Parser.Content
{
    /// <summary>
    /// Contains methods for creating <see cref="WikiComponent"/> descendants from <see cref="MwParserFromScratch"/> AST nodes.
    /// </summary>
    public interface IWikiComponentFactory
    {
        public WikiComponent CreateComponent(IWikiData rawData, WikiComponentType componentType, Node startNode, Node endNode);
        public ArticleContainer CreateArticle(IWikiData rawData, IWikiArticle article, Node startNode, Node endNode);
        public ImageContainer CreateImage(IWikiData rawData, IWikiImage image, WikiComponentType componentType, Node startNode, Node endNode);
    }
}
