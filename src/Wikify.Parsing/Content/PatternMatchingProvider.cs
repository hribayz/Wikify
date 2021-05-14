using Microsoft.Extensions.Logging;
using MwParserFromScratch;
using MwParserFromScratch.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wikify.Common.Content;

namespace Wikify.Parsing.Content
{
    /// <inheritdoc cref="IAstTranslator"/>
    public class MwAstParser : IAstTranslator
    {
        #region Patterns

        // Write the patterns with early exit if no match as highest priority.
        // Write the patterns with early exit if no match as highest priority.

        private List<Func<Node, PatternMatch>> _patterns = new()
        {
            {
                node =>
                }
        };

        #endregion

        #region Fields
        private ILogger _logger;
        #endregion

        #region Constructor
        public MwAstParser(ILogger logger)
        {
            _logger = logger;
        }
        #endregion

        #region Implementations

        /// <inheritdoc />
        public Task<IEnumerable<IWikiComponent>> TranslateNodesAsync(Node startNode)
        {
            throw new NotImplementedException();
        }

        // RULE: One component has one node or multiple nodes that are in line, one node belongs to zero or one component.

        private PatternMatch ParseNode(Node startNode)
        {
            foreach (var pattern in _patterns)
            {
                // Try to find a pattern match.
                var match = pattern(startNode);

                // If no match found, go to next pattern as early as possible.
                if (!match.IsMatch)
                {
                    continue;
                }

                // Matching pattern was found.
                else
                {
                    return match;
                }
            }

            return new PatternMatch
            {
                // Last node that has been examined is this one.
                EndNode = startNode,
                IsMatch = false,
                WikiComponent = null
            };
        }

        // Will be called to parse children of node.
        private List<WikiComponent> ParseNodes(Node startNode)
        {
            // Keep a pointer to currently examined node.
            var node = startNode;

            // Store all components found in this line.
            var components = new List<WikiComponent>();

            // Traverse the linked list of nodes by one at a time (if no match) or by more (may happen if match found).
            while (true)
            {
                /// The <see cref="node"/> points to a fresh node here.

                if (node == null)
                {
                    // Reached the end of the linked list of nodes.
                    break;
                }

                var match = ParseNode(node);
                if (match.IsMatch && match.WikiComponent == null)
                {
                    throw new ApplicationException($"WikiComponent can't be null here, it was a match.");
                }

                var children = node.EnumChildren();

                if (!children.Any())
                {
                    if (match.IsMatch)
                    {
                        // match.WikiComponent is not null here. Surpress later.
                        components.Add(match.WikiComponent);

                        // TODO: this is a bug. If some of the nodes past the first one (which has no children) have children, these will go skipped unexamined.

                        node = match.EndNode.NextNode;
                    }
                    else
                    {
                        node = node.NextNode;
                    }

                    // It is safe to early advance to next unexamined node. This node has no children.
                    continue;
                }

                var firstChild = children.First();

                // Node has children. Go one level deeper in recursion and parse all children.
                var childComponents = ParseNodes(firstChild);

                if (match.IsMatch)
                {
                    var component = match.WikiComponent;

                    // match.WikiComponent is not null here. Surpress later.
                    component.AddChild(component);
                }


                // Advance to next node, if any

                if (node.NextNode == null)
                {
                    break;
                }
                else
                {
                    node = node.NextNode;
                }
            }

            return components;
        }
        private struct PatternMatch
        {
            public bool IsMatch;

            // Last node that belongs to the component
            public Node EndNode;

            // The wiki component matched to the line of nodes
            public WikiComponent? WikiComponent;
        }
        private struct ParserEndState
        {
            // Last node that belongs to the component
            public Node EndNode;

            // The wiki component matched to the line of nodes
            public WikiComponent? WikiComponent;
        };
        #endregion
    }
}
