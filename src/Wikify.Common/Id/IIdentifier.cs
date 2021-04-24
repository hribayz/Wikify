using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wikify.Common.Content;

namespace Wikify.Common.Id
{
    public interface IIdentifier
    {
        public string Title { get; }
    }

    public interface IArticleIdentifier : IIdentifier
    {
        public LanguageEnum Language { get; }
    }
    public interface IArticleIdentifierFactory
    {
        public IArticleIdentifier GetIdentifier(string title, LanguageEnum language);
        public IArticleIdentifier GetIdentifier(Uri url);
    }
    public class ArticleIdentifierFactory : IArticleIdentifierFactory
    {
        public IArticleIdentifier GetIdentifier(string title, LanguageEnum language)
        {
            return new ArticleIdentifier(title, language);
        }

        public IArticleIdentifier GetIdentifier(Uri url)
        {
            throw new NotImplementedException();
        }
    }

    public interface IImageIdentifier : IIdentifier
    {
        public string MetadataUri { get; }
    }

}
