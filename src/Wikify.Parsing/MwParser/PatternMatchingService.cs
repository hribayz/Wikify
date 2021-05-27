using Microsoft.Extensions.Logging;
using MwParserFromScratch.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Parsing.Content;

namespace Wikify.Parsing.MwParser
{
    internal class PatternMatchingService
    {
        private IWikiContentFactory _wikiContentFactory;
        private ILogger _logger;

        // Write the patterns with early exit if no match as highest priority.
        #region Patterns
        private List<Func<Template, PatternMatch?>> _templatePatterns = new()
        {
            template =>
            {
                if (template.Name.Inlines.Any(x => x is PlainText templateName && templateName.Content.StartsWith("Infobox")))
                {
                    return new PatternMatch(WikiComponentType.InfoPanel, template);
                }
                return null;
            }
        };
        private List<Func<InlineNode, PatternMatch?>> _inlinePatterns = new()
        {

        };
        private List<Func<Node, PatternMatch?>> _patterns = new()
        {

        };

        public PatternMatchingService(ILogger logger, IWikiContentFactory wikiContentFactory)
        {
            _logger = logger;
            _wikiContentFactory = wikiContentFactory;
        }
        #endregion



        // RULE: One component has one node or multiple nodes that are adjacent siblings, one node belongs to zero or one component.
        internal bool ParseNode(Node startNode, out PatternMatchComponent? outMatchComponent)
        {
            PatternMatch? match;

            if (startNode is Template template)
            {
                match = RunPatternEnumeration(
                    _templatePatterns.Select(
                        pattern => pattern(template)));
            }
            else if (startNode is InlineNode inlineNode)
            {
                match = RunPatternEnumeration(
                    _inlinePatterns.Select(
                        pattern => pattern(inlineNode)));
            }
            else
            {
                match = RunPatternEnumeration(
                    _patterns.Select(
                        pattern => pattern(startNode)));
            }

            if (match == null)
            {
                outMatchComponent = null;
                return false;
            }

            // Match is not null here, create component.

            if (match.WikiComponentType == WikiComponentType.Image)
            {
                throw new NotImplementedException();
            }

            outMatchComponent = new PatternMatchComponent(match, _wikiContentFactory.CreateComponent(match.WikiComponentType, startNode, match.EndNode));

            _logger.LogDebug($"Pattern match: {startNode} is {match.WikiComponentType}");

            return true;
        }

        private PatternMatch? RunPatternEnumeration(IEnumerable<PatternMatch?> patternDelegates)
        {
            foreach (var patternMatch in patternDelegates)
            {

                // If no match found, go to next pattern as early as possible.
                if (patternMatch == null)
                {
                    continue;
                }

                // Matching pattern was found.
                else
                {
                    return patternMatch;
                }
            }

            return null;
        }
    }
}
