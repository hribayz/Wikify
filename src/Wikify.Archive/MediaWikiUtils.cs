using System;
using System.Collections.Generic;
using System.Text;
using Wikify.Common;
using Wikify.Common.Content;

namespace Wikify.Archive
{
    internal class MediaWikiUtils
    {
        private readonly Dictionary<LanguageEnum, string> _mediaWikiEndpoints = new()
        {
            [LanguageEnum.English] = "https://en.wikipedia.org/w/api.php?action=parse"
        };

        internal string GetParseQuery(string articleTitle, LanguageEnum language, WikiContentModel contentModel)
        {
            if (!_mediaWikiEndpoints.ContainsKey(language))
            {
                throw new NotImplementedException(nameof(MediaWikiUtils) + " does not have an implementation for language: " + language.ToString());
            }

            var endpoint = _mediaWikiEndpoints[language];

            string prop = contentModel switch
            {
                WikiContentModel.Text => "text",
                WikiContentModel.WikiText => "wikitext",
                _ => throw new NotImplementedException()
            };

            // https://en.wikipedia.org/w/api.php?action=parse&page=Article_title&prop={wikitext|text}

            return new StringBuilder()
                .Append(endpoint)
                .Append("&page=").Append(articleTitle)
                .Append("&prop=").Append(prop)
                .Append("&format=json")
                .ToString();
        }
    }
}
