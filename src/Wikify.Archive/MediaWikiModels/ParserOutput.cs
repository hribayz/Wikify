using System.Collections.Generic;

namespace Wikify.Archive
{
    public class ParserOutput
    {
        public string? title { get; set; }
        public int pageid { get; set; }
        public Dictionary<string, string>? wikitext { get; set; }
        public Dictionary<string, string>? text { get; set; }
    }

}
