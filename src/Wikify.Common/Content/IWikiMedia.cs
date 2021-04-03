using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    public interface IWikiMedia
    {
        public IIdentifier Identifier { get; }
        public ILicense License { get; }
    }

}
