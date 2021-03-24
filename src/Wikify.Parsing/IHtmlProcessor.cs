using System;
using System.Threading.Tasks;
using Wikify.Common;
using Wikify.Common.Element;

namespace Wikify.Parsing
{
    public interface IHtmlProcessor
    {
        public Task<IWikiArticleElement> ProcessHtmlAsync(string ElementHtml);
    }
    public class HtmlProcessor : IHtmlProcessor
    {
        public Task<IWikiArticleElement> ProcessHtmlAsync(string ElementHtml)
        {
            throw new NotImplementedException();
        }
    }
}
