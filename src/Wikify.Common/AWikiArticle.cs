using System.Threading.Tasks;

namespace Wikify.Common
{
    // TODO: Parameterless constuctor for DI instantiation.
    // TODO: Redo AWikiArticle asynchronously.

    /// <summary>
    /// A container for a wikipedia article. 
    /// </summary>
    public abstract class AWikiArticle
    {
        public string ArticleHtml { get; protected set; }
        public abstract Task LoadHtmlAsync(string articleHtml);
    }
}