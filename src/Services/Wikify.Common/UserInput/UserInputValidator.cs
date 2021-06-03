using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Wikify.Common.UserInput
{
    public class UserInputValidator : IUserInputValidator
    {
        public async Task<string> ValidateArticleTitleAsync(string userInput)
        {
            // TODO : do proper sanitization of user input

            // Exclude characters not permitted by the mw backend: # < > [ ] | { } _
            // Source: https://en.wikipedia.org/wiki/Wikipedia:Page_name#Technical_restrictions_and_limitations
            // Replace underscore with space if article title pasted from url.

            return Regex.Replace(userInput, @"[#<>\[\]\|{}]", "").Replace('_', ' ');
        }

        public async Task<string> ValidateUrlAsync(string userInput)
        {
            throw new NotImplementedException();
        }
    }
}
