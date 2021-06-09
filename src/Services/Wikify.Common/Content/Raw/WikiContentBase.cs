using Newtonsoft.Json;
using Wikify.Common.License;

namespace Wikify.Common.Content.Raw
{
    /// <summary>
    /// Provides common abstractions for types derived from <see cref="IWikiContent", mainly pretty-printing./>
    /// </summary>
    public abstract class WikiContentBase : IWikiContent
    {
        public ILicense License { get; private set; }
        public WikiContentBase(ILicense license)
        {
            License = license;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}