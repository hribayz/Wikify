using MwParserFromScratch.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Parser.MwParser.Specifications
{
    public abstract class PatternSpecification<T> where T : Node
    {
        public abstract Func<T, PatternMatch> Expression { get; }
    }
}
