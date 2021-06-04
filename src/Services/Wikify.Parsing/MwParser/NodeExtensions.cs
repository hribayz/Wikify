using MwParserFromScratch.Nodes;

namespace Wikify.Parsing.MwParser
{
    internal static class NodeExtensions
    {
        internal static string GetTemplateName(this Template template) => template.Name.Inlines.FirstNode.ToPlainText().Trim();
    }
}
