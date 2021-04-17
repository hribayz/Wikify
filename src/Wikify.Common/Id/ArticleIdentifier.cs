using System;
using System.Collections.Generic;
using System.IO;
using Wikify.Common.Content;

namespace Wikify.Common.Id
{
    public class ArticleIdentifier : IArticleIdentifier
    {
        private IReadOnlyDictionary<WikiContentModel, string> _endpoints;
        public string Title { get; private set; }

        public ArticleIdentifier(string title,  IReadOnlyDictionary<WikiContentModel, string> endpoints)
        {
            Title = title;
            _endpoints = endpoints;
        }

        public string GetEndpoint(WikiContentModel contentModel)
        {
            return _endpoints[contentModel];
        }
    }
}
