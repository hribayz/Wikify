using System;
using System.Threading.Tasks;
using Wikify.Common;

namespace Wikify.Parsing
{
    public interface IHtmlProcessor
    {
        public Task<AWikiArticle> ProcessArticleAsync(string ArticleHtml);
    }
    public class HtmlProcessor : IHtmlProcessor
    {
        public Task<AWikiArticle> ProcessArticleAsync(string ArticleHtml)
        {
            throw new NotImplementedException();
        }
    }
}
