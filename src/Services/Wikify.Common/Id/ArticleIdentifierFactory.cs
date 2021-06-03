using System;

namespace Wikify.Common.Id
{
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

}
