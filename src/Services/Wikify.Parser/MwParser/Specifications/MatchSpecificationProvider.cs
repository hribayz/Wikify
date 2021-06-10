using MwParserFromScratch.Nodes;
using System.Collections.Generic;

namespace Wikify.Parser.MwParser.Specifications
{
    public class MatchSpecificationProvider : IMatchSpecificationProvider
    {
        public IReadOnlyCollection<PatternSpecification<Template>> GetTemplateSpecifications()
        {
            return new List<PatternSpecification<Template>>
            {
                new InfoboxSpecification(),
            };
        }
    }
}
