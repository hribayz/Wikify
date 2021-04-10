using AngleSharp;
using System;
using System.IO;
using System.Threading.Tasks;
using Wikify.Common.Content;

namespace Wikify.Parsing.Content
{
    /// <summary>
    /// AngleSharp based implementation.
    /// </summary>
    public class ArticleParser : IArticleParser
    {
        public async Task<DirectoryInfo> CreateHtmlAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IWikiContainer<IWikiArticle>> GetArticleAsync()
        {
            throw new NotImplementedException();
        }

        public async Task LoadArticleAsync(IWikiArticle article)
        {
            // Use the default configuration for AngleSharp.
            var config = Configuration.Default;
            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(article.ArticleData));

            // Identify wiki components


        }

        public async Task LoadBuildOptionsAsync(BuildOptions buildOptions)
        {
            throw new NotImplementedException();
        }
    }
}
