using Microsoft.Extensions.Logging;
using MwParserFromScratch;
using MwParserFromScratch.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Parser.Content;

namespace Wikify.Parser.MwParser
{
    /// <inheritdoc cref="IAstParser"/>
    public class AstParser : IAstParser
    {
        private ILogger _logger;
        private IPatternMatchingService _patternMatchingService;

        // Extract node advance and children extraction to make sure they are being performed together.
        private (Node node, IEnumerable<Node> children) MoveNext((Node node, IEnumerable<Node> _) nodeTuple) => (node: nodeTuple.node.NextNode, children: nodeTuple.node.NextNode.EnumChildren());

        public AstParser(ILogger<AstParser> logger, IPatternMatchingService patternMatchingService)
        {
            _logger = logger;

            _patternMatchingService = patternMatchingService;
        }
        /// <inheritdoc />
        public async Task<List<IWikiComponent>> TranslateNodesAsync(Node startNode)
        {
            return await Task.Run(() => ParseNodes(startNode));
        }

        // Will be called to parse children of node.
        public List<IWikiComponent> ParseNodes(Node startNode)
        {
            // Make sure node and enumeration of its children belong to each other.
            // Update both when advancing.
            var node = (Node: startNode, Children: startNode.EnumChildren());

            // Store all components found in this line.
            var components = new List<IWikiComponent>();

            // Traverse the linked list of nodes by one at a time (if no match) or by more (if match spans multiple sibling nodes in a row).
            // Advance the pointer to the first unexamined node at every cycle.
            while (true)
            {
                /// The <see cref="node"/> points to a fresh node here.

                bool isMatch = _patternMatchingService.ParseNode(node.Node, out PatternMatchComponent? patternMatchComponent);

                // Pattern match at this node.
                if (isMatch)
                {
                    _logger.LogDebug("Parsing match children...");

                    // Add all children as belonging to this component.
                    var component = patternMatchComponent?.WikiComponent ??
                        throw new ApplicationException($"Pattern match should not be null here! It was a match!");

                    components.Add(component);

                    // This pattern match can span multiple nodes at the current level.
                    // Examine their children, then advance past the last one.
                    while (true)
                    {
                        var hasChildren = node.Children.Any();

                        if (hasChildren)
                        {
                            // Go one level deeper in recursion, parse whole line of descendants.
                            var childComponents = ParseNodes(node.Children.First());

                            // Add child components to the current component
                            if (childComponents.Any())
                            {
                                _logger.LogDebug($"Adding children: {childComponents.Count}.");
                                component.AddChildren(childComponents);
                            }
                        }

                        else
                        {
                            // No children, safe to advance to next node within the match.
                        }

                        // We've just examined the last node contained in the match pattern.
                        if (node.Node == patternMatchComponent.PatternMatch.EndNode)
                        {
                            _logger.LogDebug("Done parsing match children.");
                            break;
                        }

                        // The match pattern spans further in this line of nodes.
                        else
                        {
                            // Advance to the next sibling. Belongs to the same pattern.
                            node = MoveNext(node);
                        }
                    }

                    // "node" now pointing to the last node of the pattern match.
                }

                // No match at this node.
                else
                {
                    var hasChildren = node.Children.Any();

                    // No match but has children.
                    // Go deeper in recursion, try to find a match.
                    if (hasChildren)
                    {
                        var childComponents = ParseNodes(node.Children.First());

                        // Adding the childComponents directly to the container at this level will flatten the structure so that every component has children.
                        // Children from different levels that are not directly descendant can end up in the same line.
                        if (childComponents.Any())
                        {
                            _logger.LogDebug($"{nameof(ParseNodes)} returned {childComponents.Count} children. Adding to current level components list.");
                            components.AddRange(childComponents);
                        }
                    }

                    else
                    {
                        // No match, no children, safe to advance to next node on this level.
                    }
                }

                // Check if this was the last node in the linked list.
                if (node.Node.NextNode == null)
                {
                    return components;
                }

                // Advance to next node.
                node = MoveNext(node);
            }
        }


    }
}
