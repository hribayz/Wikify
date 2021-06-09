using Wikify.Common.License;

namespace Wikify.Common.Content.Raw
{
    /// <summary>
    /// A base container that provides common members for descendants (e.g. <see cref="IWikiArticle"/> and <see cref="IWikiImage"/>) representing various media types present in the runtime of the application.
    /// </summary>
    public interface IWikiContent
    {
        public ILicense License { get; }
    }
}