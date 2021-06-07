using MwParserFromScratch.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Parser.MwParser.Specifications
{
    internal abstract class PatternSpecification<T> where T : Node
    {
        internal abstract Func<T, PatternMatch> Expression { get; }

        internal PatternMatch GetMatchingPattern(T node)
        {
            return Expression(node);
        }
    }
}
