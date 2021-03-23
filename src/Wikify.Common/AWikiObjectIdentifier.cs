using System;

namespace Wikify.Common
{
    /// <summary>
    /// Smart container for data used to identify a wikipedia object.
    /// </summary>
    public abstract class AWikiObjectIdentifier
    {
        /// <summary>
        /// Gets a valid url to download a wikipedia object.
        /// </summary>
        /// <returns></returns>
        public abstract string GetUrl();
    }
}
