using System;
using System.Collections.Generic;
using Wikify.Common.Content.Raw;

namespace Wikify.Common.Content.Parsed
{
    /// <summary>
    /// A container for intrinsic information about a component (e.g. infopanel, gallery, header...) of the Wikify composition.
    /// </summary>
    public interface IWikiComponent
    {
        public IWikiData RawData { get; }
        public WikiComponentType ComponentType { get; }
        public IEnumerable<IWikiComponent> GetChildren();
        public IEnumerable<IWikiComponent> GetChildren(Predicate<IWikiComponent> filter);
    }

}
