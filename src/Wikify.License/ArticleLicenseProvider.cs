using System;
using System.Threading.Tasks;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.License
{
    public class ArticleLicenseProvider : IArticleLicenseProvider
    {

        /// <summary>
        /// Provides article license. This is a mock implementation that assumes all articles are licensed under CC BY-SA 4.0, which is the case for Wikipedia.org.
        /// </summary>
        /// <param name="articleIdentifier">Id of the article to get license for.</param>
        /// <returns>Licensing information.</returns>
        public Task<ILicense> GetArticleLicenseAsync(IArticleIdentifier articleIdentifier)
        {
            throw new NotImplementedException();
        }

    }
}
