using System;
using System.Collections.Generic;
using System.Linq;
using Wikify.Common.Content;

namespace Wikify.Parsing.Content
{
    public class WikiComponent : IWikiComponent
    {
        public WikiComponentType ComponentType { get; private set; }
        protected IReadOnlyCollection<IWikiComponent> _children { get; private set; }
        public WikiComponent(WikiComponentType componentType, IReadOnlyCollection<IWikiComponent> children)
        {
            ComponentType = componentType;
            _children = children;
        }

        public virtual IEnumerable<IWikiComponent> GetChildren(Func<IWikiComponent, bool> filter)
        {
            return _children.Where(filter);
        }
    }
}
