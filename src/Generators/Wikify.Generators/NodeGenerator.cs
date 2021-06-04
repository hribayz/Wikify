using System.Collections.Generic;
using MwParserFromScratch;
using MwParserFromScratch.Nodes;

namespace Wikify.Generators
{
    public class NodeGenerator
    {
        private WikitextParser _wikiTextParser;

        public NodeGenerator(WikitextParser wikiTextParser)
        {
            _wikiTextParser = wikiTextParser;
        }

        public IEnumerable<Template> GetTemplatesFromNode(Node astRoot)
        {
            List<Template> templates = new List<Template>();

            foreach (var child in astRoot.EnumChildren())
            {
                if (child is Template template)
                {
                    templates.Add(template);
                }

                var childTemplates = GetTemplatesFromNode(child);
                templates.AddRange(childTemplates);
            }

            return templates;
        }
    }
}
