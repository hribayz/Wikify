using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Id;

namespace Wikify.Archive
{
    public interface IArticleArchive
    {
        public Task<IWikiArticle> GetArticleAsync(IArticleIdentifier articleIdentifier, WikiContentModel contentModel);
    }
    public interface IImageArchive
    {
        public Task<IWikiImage> GetImageAsync(IImageIdentifier imageIdentifier);
    }
}