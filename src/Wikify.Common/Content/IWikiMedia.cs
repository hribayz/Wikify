using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    /// <summary>
    /// A base container that provides common members for descendants (e.g. <see cref="IWikiArticle"/> and <see cref="IWikiImage"/>) representing various media types present in the runtime of the application.
    /// </summary>
    public interface IWikiMedia
    {
        public ILicense License { get; }
    }
}