using System;
using System.Collections.Generic;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    /// <summary>
    /// A container for intrinsic information about a component (e.g. infopanel, gallery, header...) of the Wikify composition.
    /// </summary>
    public interface IWikiComponent
    {
        public WikiComponentType ComponentType { get; }
        public IEnumerable<IWikiComponent> GetChildren();
        public IEnumerable<IWikiComponent> GetChildren(Predicate<IWikiComponent> filter);

    }
}
