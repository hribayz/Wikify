using MwParserFromScratch.Nodes;
using Wikify.Common.Content;

namespace Wikify.Parsing.MwParser
{
    internal class PatternMatch
    {
        // First node that belongs to the component
        //internal Node StartNode;

        // Last node that belongs to the component
        internal Node EndNode;

        // The wiki component matched to the line of nodes
        internal WikiComponentType WikiComponentType;

        internal PatternMatch(WikiComponentType componentType, /*Node startNode,*/ Node endNode)
        {
            //StartNode = startNode;
            EndNode = endNode;
            WikiComponentType = componentType;
        }
    }
}
