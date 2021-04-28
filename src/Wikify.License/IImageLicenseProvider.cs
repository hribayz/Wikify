using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Wikify.Common;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.License
{
    public interface IImageLicenseProvider
    {
        public Task<ILicense> GetImageLicenseAsync(IImageIdentifier imageIdentifier);
        public Task<ILicense> GetImageLicenseAsync(IImageIdentifierWithMetadata imageIdentifier);
        public Task<IImmutableDictionary<IImageIdentifier, ILicense>> GetImageLicensesAsync(IEnumerable<IImageIdentifier> imageIdentifiers);
        public Task<IImmutableDictionary<IImageIdentifier, ILicense>> GetImageLicensesAsync(IEnumerable<IImageIdentifierWithMetadata> imageIdentifiers);
    }
    public interface IArticleLicenseProvider
    {
        public Task<ILicense> GetArticleLicenseAsync(IArticleIdentifier articleIdentifier);
    }
}
