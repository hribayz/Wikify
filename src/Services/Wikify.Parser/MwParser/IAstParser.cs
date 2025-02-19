﻿using MwParserFromScratch.Nodes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content.Parsed;

namespace Wikify.Parser.MwParser
{
    /// <summary>
    /// Maps MediaWiki AST onto a tree of <see cref="IWikiComponent"/>s elements. Provides methods for interaction with the resulting <see cref="IWikiComponent"/>s tree.
    /// </summary>
    public interface IAstParser
    {
        /// <summary>
        /// Maps a root of an AST subtree onto an array of same level <see cref="IWikiComponent"/>s.
        /// </summary>
        /// <param name="root">Root of an AST subtree to get matching <see cref="IWikiComponent"/> structure for.</param>
        /// <returns>Matching Wiki composition components.</returns>
        //public Task<IEnumerable<IWikiComponent>> TranslateRootNodeAsync(Wikitext root);


        /// <summary>
        /// Maps a continuous subset of an AST subtree descendants onto an array of same level <see cref="IWikiComponent"/>s. Starting with the head node, following the linked list of nodes to the end.
        /// </summary>
        /// <param name="startNode">Head of linked list of descendants.</param>
        /// <returns>Matching Wiki composition components.</returns>
        public Task<List<IWikiComponent>> TranslateNodesAsync(Node startNode);


        /// <summary>
        /// Maps a continuous subset of an AST subtree descendants onto an array of same level <see cref="IWikiComponent"/>s. Starting with the head node, following linked list of nodes until the end node is hit.
        /// </summary>
        /// <param name="startNode">Head of linked list of descendants.</param>
        /// <param name="endNode">Last node of the same linked list of descendants to include in the matching.</param>
        /// <returns></returns>
        //public Task<IEnumerable<IWikiComponent>> TranslateNodesAsync(Node startNode, Node endNode);
    }
}
