using System;

namespace Wikify.Common.Id
{
    public interface IArticleIdentifierFactory
    {
        public IArticleIdentifier GetIdentifier(string title, LanguageEnum language);
        public IArticleIdentifier GetIdentifier(Uri url);
    }
}
