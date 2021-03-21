using System.Threading.Tasks;

namespace Wikify.Common
{
    // TODO: Parameterless constuctor for DI instantiation.
    // TODO: Redo AWikiArticle asynchronously.

    /// <summary>
    /// Smart container for a wikipedia article. 
    /// </summary>
    public abstract class AWikiArticle
    {
        public string ArticleHtml { get; protected set; }
        public AWikiArticle(string articleHtml)
        {
            LoadHtml(articleHtml);
        }
        public abstract void LoadHtml(string articleHtml);
    }
    public class WikiArticle : AWikiArticle
    {
        public WikiArticle(string articleHtml) : base(articleHtml)
        {

        }

        public override void LoadHtml(string articleHtml)
        {
            throw new System.NotImplementedException();
        }
    }
}