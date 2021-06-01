using MwParserFromScratch.Nodes;

namespace Wikify.Parsing.MwParser
{
    public interface IPatternMatchingService
    {
        public bool ParseNode(Node startNode, out PatternMatchComponent? outMatchComponent);

    }
}