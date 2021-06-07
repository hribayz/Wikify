using System.Threading.Tasks;
using Wikify.Common.Domain;
using Wikify.Common.Id;

namespace Wikify.Common.Id
{
    public interface IArticleIdParser
    {
        public Task<IArticleIdentifier> GetIdentifierAsync(string articleTitle, LanguageEnum language);
        public Task<IArticleIdentifier> GetIdentifierAsync(string articleUrl);
    }
}
