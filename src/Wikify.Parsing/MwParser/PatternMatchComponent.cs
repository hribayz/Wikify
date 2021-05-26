using Wikify.Parsing.Content;

namespace Wikify.Parsing.MwParser
{
    internal class PatternMatchComponent
    {
        internal PatternMatch PatternMatch;
        internal WikiComponent WikiComponent;
        internal PatternMatchComponent(PatternMatch patternMatch, WikiComponent wikiComponent)
        {
            PatternMatch = patternMatch;
            WikiComponent = wikiComponent;
        }
    }
}
