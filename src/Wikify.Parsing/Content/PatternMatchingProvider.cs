using Microsoft.Extensions.Logging;
using MwParserFromScratch.Nodes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wikify.Common.Content;

namespace Wikify.Parsing.Content
{
    /// <inheritdoc cref="IAstTranslator"/>
    public class PatternMatchingProvider : IAstTranslator
    {
        #region Patterns
        private Dictionary<WikiComponentType, List<Predicate<Node>>> _patterns = new()
        {
            [WikiComponentType.Article] = new List<Predicate<Node>>
            {
                node => true
            }
        };
        #endregion

        #region Fields
        private ILogger _logger;
        #endregion

        #region Constructor
        public PatternMatchingProvider(ILogger logger)
        {
            _logger = logger;
        }
        #endregion

        #region Implementations
        /// <inheritdoc />
        public Task<IEnumerable<IWikiComponent>> TranslateNodeAsync(Wikitext root)
        {
            if (root is null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            // TODO: implement breadth-first search for patterns as recursive method.




            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<IEnumerable<IWikiComponent>> TranslateNodesAsync(Node startNode, Node endNode)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<IEnumerable<IWikiComponent>> TranslateNodesAsync(Node startNode)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
