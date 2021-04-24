using System;
using System.Collections.Generic;
using System.Text;
using Wikify.Common;
using Wikify.Common.Content;

namespace Wikify.Common.Network
{
    public static class MediaWikiUtils
    {
        private const string _mediaWikiApiUrl = "https://en.wikipedia.org/w/api.php";

        private static readonly Dictionary<LanguageEnum, string> _mediaWikiEndpoints = new()
        {
            [LanguageEnum.English] = _mediaWikiApiUrl + "?action=parse"
        };

        public static string GetParseQuery(string articleTitle, LanguageEnum language, WikiContentModel contentModel)
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

            return new StringBuilder()
                .Append(endpoint)
                .Append("&page=").Append(articleTitle)
                .Append("&prop=").Append(prop)
                .Append("&format=json")
                .ToString();
        }

        public static string GetImageMetadataQuery(IEnumerable<string> titles)
        {
            var querySb = new StringBuilder()
                .Append(_mediaWikiApiUrl)
                .Append("?action=query")
                .Append("&prop=imageinfo")
                .Append("&iiprop=extmetadata")
                .Append("&format=json")
                .Append("&titles=");

            foreach (var title in titles)
            {
                querySb.Append(title).Append("|");
            }

            // remove the trailing "|" that is now the last char
            querySb.Remove(querySb.Length - 1, 1);
            return querySb.ToString();
        }

    }
}
