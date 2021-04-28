using System.Collections.Generic;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    /// <summary>
    /// A container for intrinsic information about a part (e.g. infopanel, gallery, header...) of the Wikify composition.
    /// </summary>
    public interface IWikiComponent
    {
        public IEnumerable<IWikiComponent> Children { get; }
        public WikiComponentType ComponentType { get; }
    }
}
