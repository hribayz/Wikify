using System.Threading.Tasks;

namespace Wikify.Common
{
    // TODO: Parameterless constuctor for DI instantiation.
    // TODO: Redo AWikiArticle asynchronously.

    /// <summary>
    /// A container for a wikipedia article. Implement as smart container including logic (breaking into elements...) or simple conainer with logic segregated elsewhere (visitor pattern)
    /// </summary>
    public abstract class AWikiArticle
    {
        public string ArticleHtml { get; protected set; }
        public IArticleElementCollection Elements { get; protected set; }
    }
}