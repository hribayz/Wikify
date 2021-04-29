﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Common.MediaWikiModels
{
    public static class MediaWikiParserResponse
    {
        public class ParserRootObject
        {
            public ParserOutput? parse { get; set; }
        }

        public class ParserOutput
        {
            public string? title { get; set; }
            public int pageid { get; set; }
            public Dictionary<string, string>? wikitext { get; set; }
            public Dictionary<string, string>? text { get; set; }
        }
    }

}
