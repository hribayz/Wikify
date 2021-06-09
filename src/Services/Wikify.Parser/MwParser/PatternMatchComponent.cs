using Wikify.Parser.Content;

namespace Wikify.Parser.MwParser
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
