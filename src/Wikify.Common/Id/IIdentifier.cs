using System;
using Wikify.Common.Content.Types;

namespace Wikify.Common.Id
{
    /// <summary>
    /// Container for data that identifies a wikipedia object, mainly where to get it.
    /// </summary>
    public interface IIdentifier<T> where T : AWikiContent
    {

        /// <summary>
        /// Gets a valid url to download a wikipedia object.
        /// </summary>
        /// <returns></returns>
        public string GetUrl();
        public string GetFileName();
    }
}
