using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Archive
{
    public class MediaWikiResponse
    {
        public Parse? parse { get; set; }
    }

    public class Parse
    {
        public string? title { get; set; }
        public int pageid { get; set; }
        public Wikitext? wikitext { get; set; }
        public Text? text { get; set; }
    }

    public class Wikitext
    {
        public string? _ { get; set; }
    }

    public class Text
    {
        public string? _ { get; set; }
    }

}
