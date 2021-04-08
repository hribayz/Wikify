using System;
using System.IO;
using System.Threading.Tasks;
using Wikify.Common.Content;

namespace Wikify.Parsing.Content
{
    public class ArticleParser : IArticleParser
    {
        public async Task<DirectoryInfo> GetArticleAsync(BuildOptions buildOptions)
        {
            throw new NotImplementedException();
        }

        public async Task LoadArticleAsync(IWikiArticle article)
        {
            throw new NotImplementedException();
        }

    }
}
