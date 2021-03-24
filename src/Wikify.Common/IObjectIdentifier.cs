using System;

namespace Wikify.Common
{
    /// <summary>
    /// Container for data used to identify a wikipedia object.
    /// </summary>
    public interface IObjectIdentifier
    {
        /// <summary>
        /// Gets a valid url to download a wikipedia object.
        /// </summary>
        /// <returns></returns>
        public string GetUrl();
    }
}
