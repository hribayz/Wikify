using System;
using Wikify.Common.Content;

namespace Wikify.Common.Id
{
    public class ArticleIdentifier : IArticleIdentifier
    {
        public string Title => throw new NotImplementedException();

        public Uri GetEndpoint(WikiContentModel contentModel)
        {
            throw new NotImplementedException();
        }
    }
}
