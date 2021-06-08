using MwParserFromScratch.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Domain;

namespace Wikify.Parser.MwParser.Specifications
{
    public class InfoboxSpecification : Pattern<Template>
    {
        private Func<Template, PatternMatch> _expression = 
            template =>
            {
                if (template.GetTemplateName().ToLower().StartsWith("infobox"))
                {
                    return new PatternMatch(WikiComponentType.InfoPanel, template);
                }
                else
                {
                    return PatternMatch.None;
                }
            };

        public override Func<Template, PatternMatch> Expression => _expression;
    }
}
