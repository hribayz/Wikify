using MwParserFromScratch.Nodes;

namespace Wikify.Parsing.MwParser
{
    internal interface IPatternMatchingService
    {
        public bool ParseNode(Node startNode, out PatternMatchComponent? outMatchComponent);

    }
}