using Microsoft.Extensions.Logging;
using MwParserFromScratch.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Parser.Content;
using Wikify.Parser.MwParser.Specifications;

namespace Wikify.Parser.MwParser
{

    internal class PatternMatchingService : IPatternMatchingService
    {
        private ILogger _logger;
        private IWikiContentFactory _wikiContentFactory;

        private IReadOnlyCollection<Pattern<Template>> _templateSpecifications;

        public PatternMatchingService(ILogger<PatternMatchingService> logger, IWikiContentFactory wikiContentFactory, ISpecificationProvider specificationProvider)
        {
            _logger = logger;
            _wikiContentFactory = wikiContentFactory;

            _templateSpecifications = specificationProvider.GetTemplateSpecifications();
        }

        // RULE: One component has one node or multiple nodes that are adjacent siblings, one node belongs to zero or one component.
        public bool ParseNode(Node startNode, out PatternMatchComponent? outMatchComponent)
        {
            PatternMatch? match = null;

            if (startNode is Template template)
            {
                match = RunPatternEnumeration(
                    _templateSpecifications.Select(
                        pattern => pattern.GetPatternMatch(template)));
            }

            //else if (startNode is InlineNode inlineNode)
            //{
            //    match = RunPatternEnumeration(
            //        _inlinePatterns.Select(
            //            pattern => pattern(inlineNode)));
            //}
            //else
            //{
            //    match = RunPatternEnumeration(
            //        _patterns.Select(
            //            pattern => pattern(startNode)));
            //}

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

            #region Log match

            var startNodeString = startNode.ToString() ?? "";
            var startNodeSubstring = startNodeString.Substring(0, Math.Min(50, startNodeString.Length));
            _logger.LogDebug($"Found {match.WikiComponentType}.{Environment.NewLine}" +
                $"Starts with: {startNodeSubstring}.{Environment.NewLine}" +
                $"Has length of: {startNodeString.Length}.{Environment.NewLine}" +
                $"Has hash: {startNodeString.GetHashCode()}");

            #endregion

            return true;
        }

        private PatternMatch? RunPatternEnumeration(IEnumerable<PatternMatch?> matchResults)
        {
            foreach (var patternMatch in matchResults)
            {
                // If no match found, go to next pattern as early as possible.
                if (patternMatch == PatternMatch.None)
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
