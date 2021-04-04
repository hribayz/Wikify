using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Wikify.Archive;
using Wikify.Common.Content;
using Wikify.Common.Id;

namespace Wikify.Test.Archive
{

    public class ArticleArchiveTester
    {
        IArchive<IWikiArticle> _articleArchive;
        public ArticleArchiveTester(IArchive<IWikiArticle> articleArchive)
        {
            _articleArchive = articleArchive;
        }
        public async Task<IWikiArticle> GetArticleAsync(IIdentifier<IWikiArticle> articleIdentifier)
        {
            return await _articleArchive.GetMediaAsync(articleIdentifier);
        }
    }
}
