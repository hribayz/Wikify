using Wikify.Common;

namespace Wikify.Archive
{
    interface IWikiArticleProvider
    {
        AWikiArticle GetArticle(AWikiArticleIdentifier articleIdentifier);
    }
}