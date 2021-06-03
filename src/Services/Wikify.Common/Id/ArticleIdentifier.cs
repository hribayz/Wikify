using System;
using System.Collections.Generic;
using System.IO;
using Wikify.Common.Content;

namespace Wikify.Common.Id
{
    public class ArticleIdentifier : IArticleIdentifier
    {
        public string Title { get; private set; }

        public LanguageEnum Language { get; private set; }

        public ArticleIdentifier(string title, LanguageEnum language)
        {
            Title = title;
            Language = language;
        }
    }

}
