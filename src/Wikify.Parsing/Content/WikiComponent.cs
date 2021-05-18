using MwParserFromScratch.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using Wikify.Common.Content;

namespace Wikify.Parsing.Content
{
    public class WikiComponent : IWikiComponent
    {
        // Getter is protected because field is supposed to be accessed by GetChildren() method (so not internal/public), but descendants can implement overrides, so may need access to field.
        protected LinkedList<IWikiComponent> _children { get; private set; }

        // Getter is internal because it's a matter of implementation rather than part of this module's public interface, also no dependency on MwParserFromScratch is supposed to leak from this assembly.
        internal Node StartNode { get; private set; }
        internal Node EndNode { get; private set; }
        public WikiComponentType ComponentType { get; private set; }

        internal void AddChild(WikiComponent wikiComponent)
        {
            _children.AddLast(wikiComponent);
        }
        internal void AddChildren(LinkedListNode<IWikiComponent> wikiComponents)
        {
            _children.AddLast(wikiComponents);
        }

        public WikiComponent(WikiComponentType componentType, Node startNode, Node endNode)
        {
            ComponentType = componentType;
            StartNode = startNode;
            EndNode = endNode;

            _children = new LinkedList<IWikiComponent>();
        }

        public virtual IEnumerable<IWikiComponent> GetChildren(Predicate<IWikiComponent> filter)
        {
            return _children.Where(x => filter(x));
        }

    }
}
