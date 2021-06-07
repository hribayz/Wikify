using System;
using Wikify.Common.Domain;

namespace Wikify.Common.Id
{
    public interface IArticleIdentifierFactory
    {
        public IArticleIdentifier GetIdentifier(string title, LanguageEnum language);
        public IArticleIdentifier GetIdentifier(Uri url);
    }
}
