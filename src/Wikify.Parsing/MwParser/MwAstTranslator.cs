using Microsoft.Extensions.Logging;
using MwParserFromScratch;
using MwParserFromScratch.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Parsing.Content;

namespace Wikify.Parsing.MwParser
{
    /// <inheritdoc cref="IAstTranslator"/>
    public class MwAstTranslator : IAstTranslator
    {

        private ILogger _logger;

        private PatternMatchingService _patternMatchingService;
        public MwAstTranslator(ILogger logger, IWikiContentFactory wikiContentFactory)
        {
            _logger = logger;

            _patternMatchingService = new PatternMatchingService(wikiContentFactory);
        }
        /// <inheritdoc />
        public async Task<LinkedList<IWikiComponent>> TranslateNodesAsync(Node startNode)
        {
            return await Task.Run(() => ParseNodes(startNode));
        }

        // Will be called to parse children of node.
        private LinkedList<IWikiComponent> ParseNodes(Node startNode)
        {
            // Keep a pointer to currently examined node.
            var node = startNode;

            // Keep the enumerable of children, evaluate every time the node advances.
            var children = node.EnumChildren();

            // Store all components found in this line.
            var components = new LinkedList<IWikiComponent>();

            // Traverse the linked list of nodes by one at a time (if no match) or by more (if match spans multiple sibling nodes in a row).
            // Advance the pointer to the first unexamined node at every cycle.
            while (true)
            {
                /// The <see cref="node"/> points to a fresh node here.

                bool isMatch = _patternMatchingService.ParseNode(node, out PatternMatchComponent? patternMatchComponent);

                // Pattern match at this node.
                if (isMatch)
                {
                    // Add all children as belonging to this component.
                    var component = patternMatchComponent?.WikiComponent ??
                        throw new ApplicationException($"Pattern match should not be null here! It was a match!");

                    components.AddLast(component);

                    // This pattern match can span multiple nodes at the current level.
                    // Examine their children, then advance past the last one.
                    while (true)
                    {
                        var hasChildren = children.Any();

                        if (hasChildren)
                        {
                            // Go one level deeper in recursion, parse whole line of descendants.
                            var childComponents = ParseNodes(children.First());

                            // Add child components to the current component
                            if (childComponents.First != null)
                            {
                                component.AddChildren(childComponents.First);
                            }
                        }

                        else
                        {
                            // No children, safe to advance to next node within the match.
                        }

                        // We've just examined the last node contained in the match pattern.
                        if (node == patternMatchComponent.PatternMatch.EndNode)
                        {
                            break;
                        }

                        // The match pattern spans further in this line of nodes.
                        else
                        {
                            node = node.NextNode;
                        }
                    }

                    // "node" now pointing to the last node of the pattern match.
                }

                // No match at this node.
                else
                {
                    var hasChildren = children.Any();

                    // No match but has children.
                    // Go deeper in recursion, try to find a match.
                    if (hasChildren)
                    {
                        var childComponents = ParseNodes(children.First());

                        // Adding the childComponents directly to the container at this level will flatten the structure so that every component has children.
                        // Children from different levels that are not directly descendant can end up in the same line.
                        if (childComponents.Any())
                        {
                            components.AddLast()
                            components.AddLast(childComponents.First ??
                                throw new ApplicationException($"Components linked list can't contain null element."));
                        }
                    }

                    else
                    {
                        // No match, no children, safe to advance to next node on this level.
                    }
                }

                // Check if this was the last node in the linked list.
                if (node.NextNode == null)
                {
                    return components;
                }

                // Advance to next node.
                node = node.NextNode;
            }
        }
    }
}
