using MwParserFromScratch.Nodes;
using System;
using Wikify.Common.Content.Parsed;

namespace Wikify.Parser.MwParser
{
    public class PatternMatch
    {
        // First node that belongs to the component
        //internal Node StartNode;

        private Node? _endNode;

        // Last node that belongs to the component
        public Node EndNode
        {
            get
            {
                if (_endNode == null)
                {
                    throw new InvalidOperationException($"Can't access {nameof(EndNode)} property of default node.");
                }
                return _endNode;
            }
            private set
            {
                _endNode = value;
            }
        }


        // The wiki component matched to the line of nodes
        public WikiComponentType WikiComponentType;

        public PatternMatch(WikiComponentType componentType, Node endNode)
        {
            EndNode = endNode ?? throw new ArgumentNullException(nameof(endNode));
            WikiComponentType = componentType;
        }

        // To create the None singleton
        private PatternMatch()
        {
            WikiComponentType = WikiComponentType.None;
        }

        public static PatternMatch None = new PatternMatch();
    }
}
