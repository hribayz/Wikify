using MwParserFromScratch.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Parser.MwParser.Specifications
{
    interface IMatchSpecificationProvider
    {
        public IReadOnlyCollection<PatternSpecification<Template>> GetTemplateSpecifications();
    }
}
