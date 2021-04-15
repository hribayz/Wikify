using AngleSharp;
using System;
using System.IO;
using System.Threading.Tasks;
using Wikify.Common.Content;
using MwParserFromScratch;
using MwParserFromScratch.Nodes;

namespace Wikify.Parsing.Content
{
    /// <summary>
    /// 
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
            if (article.ContentModel != WikiContentModel.WikiText)
            {
                throw new NotImplementedException();
            }

            var parser = new WikitextParser();
            var ast = parser.Parse(article.ArticleData);

            throw new NotImplementedException();

        }

        public async Task LoadBuildOptionsAsync(BuildOptions buildOptions)
        {
            throw new NotImplementedException();
        }
    }
}
