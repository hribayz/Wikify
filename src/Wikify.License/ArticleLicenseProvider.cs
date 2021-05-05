using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Wikify.Common.Id;
using Wikify.Common.License;
using Wikify.License.Copyright;

namespace Wikify.License
{
    /// <inheritdoc cref="IArticleLicenseProvider"/>
    public class ArticleLicenseProvider : IArticleLicenseProvider
    {
        private ILogger _logger;
        private ILicenseFactory _licenseFactory;
        private ICopyrightFactory _copyrightFactory;

        public ArticleLicenseProvider(ILogger logger, ILicenseFactory licenseFactory, ICopyrightFactory copyrightFactory)
        {
            _logger = logger;
            _licenseFactory = licenseFactory;
            _copyrightFactory = copyrightFactory;
        }

        /// <summary>
        /// Provides article license. This is a mock implementation that assumes all articles are licensed under CC BY-SA 4.0, which is the case for Wikipedia.org.
        /// </summary>
        /// <param name="articleIdentifier">Id of the article to get license for.</param>
        /// <returns>Licensing information.</returns>
        public async Task<ILicense> GetArticleLicenseAsync(IArticleIdentifier articleIdentifier)
        {
            var copyright = _copyrightFactory.CreateCopyright(CopyrightLicenseEnum.CcBySa);
            var attribution = _copyrightFactory.CreateAttributionWithoutAuthor(articleIdentifier.Title, "www.wikipedia.org");
            return _licenseFactory.CreateLicense(copyright, attribution, LicenseRestrictionsEnum.None);
        }

    }
}
