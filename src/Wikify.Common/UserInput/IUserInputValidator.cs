using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Common.UserInput
{
    public interface IUserInputValidator
    {
        public Task<string> ValidateArticleTitleAsync(string userInput);
        public Task<string> ValidateUrlAsync(string userInput);
    }
}
