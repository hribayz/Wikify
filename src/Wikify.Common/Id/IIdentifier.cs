using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content;

namespace Wikify.Common.Id
{
    public interface IIdentifier
    {
        public string Url { get; }
    }
    public interface IIdentifier<T> : IIdentifier where T : IWikiMedia
    {

    }
    public class ArticleIdentifier : IIdentifier<IWikiArticle>
    {
        public ArticleIdentifier(string url)
        {
            Url = url;
        }
        public string Url { get; private set; }
    }
}
