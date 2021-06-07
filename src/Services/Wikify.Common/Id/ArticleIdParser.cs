using System;
using System.Threading.Tasks;
using System.Web;
using Wikify.Common.Domain;
using Wikify.Common.UserInput;

namespace Wikify.Common.Id
{
    public class ArticleIdParser : IArticleIdParser
    {
        private IUserInputValidator _userInputValidator;
        private IArticleIdentifierFactory _articleIdentifierFactory;
        public ArticleIdParser(IUserInputValidator userInputValidator, IArticleIdentifierFactory articleIdentifierFactory)
        {
            _userInputValidator = userInputValidator;
            _articleIdentifierFactory = articleIdentifierFactory;
        }
        // https://en.wikipedia.org/w/api.php?action=parse&page=Early_life_of_Vladimir_Lenin&format=json
        public async Task<IArticleIdentifier> GetIdentifierAsync(string articleTitle)
        {
            throw new NotImplementedException();
        }

        public async Task<IArticleIdentifier> GetIdentifierAsync(string articleTitle, LanguageEnum language)
        {
            var validatedTitle = await _userInputValidator.ValidateArticleTitleAsync(articleTitle);

            return _articleIdentifierFactory.GetIdentifier(validatedTitle, language);
        }
    }
}
