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
        /// <summary>
        /// Original data of this component.
        /// </summary>
        public IWikiData RawData { get; }
        
        /// <summary>
        /// Gets the component type of this component.
        /// </summary>
        public WikiComponentType ComponentType { get; }
        
        /// <summary>
        /// Enumerates the children of this component.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IWikiComponent> GetChildren();

        /// <summary>
        /// Enumerates the children of this component that pass given predicate.
        /// </summary>
        /// <param name="filter">Predicate to apply to each child.</param>
        /// <returns>Children that have passed the predicate.</returns>
        public IEnumerable<IWikiComponent> GetChildren(Predicate<IWikiComponent> filter);
    }

}
