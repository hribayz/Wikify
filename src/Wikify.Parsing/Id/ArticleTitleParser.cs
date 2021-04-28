using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Wikify.Common;
using Wikify.Common.Content;
using Wikify.Common.Id;

namespace Wikify.Parsing.Id
{
    public class ArticleTitleParser : IArticleIdParser
    {
        private IUserInputValidator _userInputValidator;
        private IArticleIdentifierFactory _articleIdentifierFactory;
        public ArticleTitleParser(IUserInputValidator userInputValidator, IArticleIdentifierFactory articleIdentifierFactory)
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
            var urlEncodedTitle = HttpUtility.UrlEncode(validatedTitle);

            return _articleIdentifierFactory.GetIdentifier(urlEncodedTitle, language);
        }
    }
}
