using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    public interface IWikiMedia
    {
        public ILicense License { get; }
    }
}