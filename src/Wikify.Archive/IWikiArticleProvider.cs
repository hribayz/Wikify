using System;
using Wikify.Common;

namespace Wikify.Archive
{
    public interface IWikiArticleProvider
    {
        public AWikipediaArticle GetArticle(AWikiArticleIdentifier articleIdentifier);
    }
}