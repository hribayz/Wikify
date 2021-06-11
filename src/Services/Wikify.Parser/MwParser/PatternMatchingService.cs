using Microsoft.Extensions.Logging;
using MwParserFromScratch.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content.Parsed;
using Wikify.Common.Content.Raw;
using Wikify.Parser.Content;
using Wikify.Parser.MwParser.Specifications;

namespace Wikify.Parser.MwParser
{

    internal class PatternMatchingService : IPatternMatchingService
    {
        private ILogger _logger;
        private IWikiContentFactory _wikiContentFactory;
        private IWikiComponentFactory _wikiComponentFactory;

        private IReadOnlyCollection<PatternSpecification<Template>> _templateSpecifications;

        public PatternMatchingService(ILogger<PatternMatchingService> logger, IWikiContentFactory wikiContentFactory, IWikiComponentFactory wikiComponentFactory, IMatchSpecificationProvider matchSpecificationProvider)
        {
            _logger = logger;
            _wikiContentFactory = wikiContentFactory;
            _wikiComponentFactory = wikiComponentFactory;

            _templateSpecifications = matchSpecificationProvider.GetTemplateSpecifications();
        }

        // RULE: One component has one node or multiple nodes that are adjacent siblings, one node belongs to zero or one component.
        public bool ParseNode(Node startNode, out PatternMatchComponent? outMatchComponent)
        {
            PatternMatch? match = null;

            if (startNode is Template template)
            {
                match = RunPatternEnumeration(
                    _templateSpecifications.Select(
                        pattern => pattern.Expression(template)));
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

            // We have a match here, create component.

            if (match.WikiComponentType == WikiComponentType.Image)
            {
                // TODO: Must figure out lazy loading of data like images here.
                // Probably change the T IWikiContainer.Content member to something like T LoadContentAsync().
                // Then the container will load the content itself when asked to, which means it will need an instance of IArchive and IIdProvider.
                // So maybe create a service to wrap all that and pass it to the IWikiContainerFactory?

                throw new NotImplementedException();
            }

            var rawDataSb = new StringBuilder();
            Node exportNode = startNode;

            while (true)
            {
                rawDataSb.Append(exportNode.ToPlainText());

                if (exportNode == match.EndNode)
                {
                    break;
                }
                else
                {
                    // Assuming that there is a non-null node behind one that was not the end node.
                    exportNode = exportNode.NextNode;
                }
            }

            var rawData = _wikiContentFactory.CreateWikiData(rawDataSb.ToString(), ContentModel.WikiText);

            outMatchComponent = new PatternMatchComponent(match, _wikiComponentFactory.CreateComponent(rawData, match.WikiComponentType, startNode, match.EndNode));

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
