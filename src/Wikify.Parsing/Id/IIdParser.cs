using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Wikify.Common.Content;
using Wikify.Common.Id;

namespace Wikify.Parsing.Id
{
    public interface IIdParser
    {
        public Task<IIdentifier> GetIdentifierAsync(string userInput);
    }
    public class ArticleTitleParser : IIdParser
    {
        private IUserInputValidator _userInputValidator;
        public ArticleTitleParser(IUserInputValidator userInputValidator)
        {
            _userInputValidator = userInputValidator;
        }
        // https://en.wikipedia.org/w/api.php?action=parse&page=Early_life_of_Vladimir_Lenin&format=json
        public async Task<IIdentifier> GetIdentifierAsync(string userInput)
        {
            var validatedTitle = await _userInputValidator.ValidateArticleTitleAsync(userInput);
            var noSpacesTitle = validatedTitle.Replace(' ', '_');



            var urlEncodedTitle = HttpUtility.UrlEncode(noSpacesTitle);

            var endpoints = new Dictionary<WikiContentModel, string>()
            {
                [WikiContentModel.WikiText] = ""
            };

        }
        private 
    }
}
