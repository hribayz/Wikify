using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Wikify.Archive;
using Wikify.Common.Content;
using Wikify.Common.Content.Types;
using Wikify.Common.Id;

namespace Wikify.Test.Archive
{

    public class ArticleArchiveTester
    {
        IArchive<WikiArticle> _articleArchive;
        public ArticleArchiveTester(IArchive<WikiArticle> articleArchive)
        {
            _articleArchive = articleArchive;
        }
        public async Task<IContainer<WikiArticle>> GetArticleAsync(IIdentifier<WikiArticle> articleIdentifier)
        {
            return await _articleArchive.GetElementAsync(articleIdentifier);
        }
    }
}
