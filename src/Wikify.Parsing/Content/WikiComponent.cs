using MwParserFromScratch.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using Wikify.Common.Content;

namespace Wikify.Parsing.Content
{
    public class WikiComponent : IWikiComponent
    {
        protected IReadOnlyCollection<IWikiComponent> _children { get; private set; }
        public WikiComponentType ComponentType { get; private set; }
        public WikiComponent(WikiComponentType componentType, IReadOnlyCollection<IWikiComponent> children)
        {
            _children = children;
            ComponentType = componentType;
        }

        public virtual IEnumerable<IWikiComponent> GetChildren(Predicate<IWikiComponent> filter)
        {
            return _children.Where(x => filter(x));
        }
    }
}
