using Newtonsoft.Json;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    /// <summary>
    /// Provides common abstractions for types derived from <see cref="IWikiMedia", mainly pretty-printing./>
    /// </summary>
    public abstract class WikiMediaBase : IWikiMedia
    {
        public ILicense License { get; private set; }
        public WikiMediaBase(ILicense license)
        {
            License = license;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}