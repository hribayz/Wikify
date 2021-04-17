using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Wikify.Archive;
using Wikify.Common.Content;
using Wikify.Common.Id;

namespace Wikify.Test.Archive
{
    public class ArticleArchiveTester
    {
        IArticleArchive _articleArchive;
        public ArticleArchiveTester(IArticleArchive articleArchive)
        {
            _articleArchive = articleArchive;
        }
        public async Task<IWikiArticle> GetArticleAsync(IArticleIdentifier articleIdentifier)
        {
            return await _articleArchive.GetArticleAsync(articleIdentifier, WikiContentModel.Text);
        }
    }
}
