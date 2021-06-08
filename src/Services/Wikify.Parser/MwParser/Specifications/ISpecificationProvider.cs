using MwParserFromScratch.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Parser.MwParser.Specifications
{
    interface ISpecificationProvider
    {
        public IReadOnlyCollection<Pattern<Template>> GetTemplateSpecifications();
    }
    public class SpecificationProvider : ISpecificationProvider
    {
        public IReadOnlyCollection<Pattern<Template>> GetTemplateSpecifications()
        {
            return new List<Pattern<Template>>
            {
                new InfoboxSpecification(),
            };
        }
    }
}
