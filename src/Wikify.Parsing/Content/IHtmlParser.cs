using System;
using System.Threading.Tasks;
using Wikify.Common;
using Wikify.Common.Content;

namespace Wikify.Parsing.Content
{
    public interface IHtmlParser
    {
        public Task<IElement> ProcessHtmlAsync(string ElementHtml);
    }
    public class HtmlProcessor : IHtmlParser
    {
        public Task<IElement> ProcessHtmlAsync(string ElementHtml)
        {
            throw new NotImplementedException();
        }
    }
}
