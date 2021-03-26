using System;
using Wikify.Common.Content.Types;

namespace Wikify.Common.Id
{
    /// <summary>
    /// Container for data that identifies a wikipedia object, mainly where to get it.
    /// </summary>
    public interface IIdentifier
    {
        /// <summary>
        /// Gets a valid url to download a wikipedia object.
        /// </summary>
        /// <returns></returns>
        public string GetUrl();
    }
    /// <summary>
    /// Container for data that identifies a wikipedia object with content of specific WikiObject type like image or text.
    /// </summary>
    public interface IIdentifier<T> : IIdentifier where T : AWikiContent
    {

    }

}
