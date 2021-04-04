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
        public string FileName { get; }
    }
    public interface IIdentifier<T> : IIdentifier where T : IWikiMedia
    {

    }
    public class ArticleIdentifier : IIdentifier<IWikiArticle>
    {
        public string Url => throw new NotImplementedException();

        public string FileName => throw new NotImplementedException();
    }
}
