using MwParserFromScratch.Nodes;

namespace Wikify.Parsing.MwParser
{
    /// <summary>
    /// Provides methods for identification of wikify composition components.
    /// </summary>
    public interface IPatternMatchingService
    {
        public bool ParseNode(Node startNode, out PatternMatchComponent? outMatchComponent);

    }
}