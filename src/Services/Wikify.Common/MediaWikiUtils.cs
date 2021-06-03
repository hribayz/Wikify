using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wikify.Common;
using Wikify.Common.Content;
using Wikify.Common.MediaWikiModels;
using static Wikify.Common.MediaWikiModels.MediaWikiImageInfoResponse;

namespace Wikify.Common
{
    public static class MediaWikiUtils
    {
        private const string _mediaWikiApiUrl = "https://www.mediawiki.org/w/api.php";
        private const string _wikipediaApiUrl = "https://LANG.wikipedia.org/w/api.php";

        private static readonly Dictionary<LanguageEnum, string> _wikipediaApiEndpoints = new()
        {
            [LanguageEnum.English] = _wikipediaApiUrl.Replace("LANG", "en") + "?action=parse"
        };

        public static string GetParseQuery(string articleTitle, LanguageEnum language, TextContentModel contentModel)
        {
            if (!_wikipediaApiEndpoints.ContainsKey(language))
            {
                throw new NotImplementedException(nameof(MediaWikiUtils) + " does not have an implementation for language: " + language.ToString());
            }

            var endpoint = _wikipediaApiEndpoints[language];

            string prop = contentModel switch
            {
                TextContentModel.Text => "text",
                TextContentModel.WikiText => "wikitext",
                _ => throw new NotImplementedException()
            };

            return new StringBuilder()
                .Append(endpoint)
                .Append("&page=").Append(articleTitle)
                .Append("&prop=").Append(prop)
                .Append("&format=json")
                .ToString();
        }

        public static string GetImageMetadataQuery(IEnumerable<string> titles, MediaWikiImageInfoProps iiProps)
        {
            #region Argument validation

            if (titles is null)
            {
                throw new ArgumentNullException(nameof(titles));
            }

            if (!titles.Any())
            {
                throw new ArgumentException("Cannot query metadata for empty ienumerable of titles");
            }

            if (iiProps.HasFlag(MediaWikiImageInfoProps.None))
            {
                throw new ArgumentException("Cannot query metadata with no props");
            }

            #endregion

            // compose iiProps argument value from raised flags
            var propsSb = new StringBuilder();
            foreach (MediaWikiImageInfoProps flag in Enum.GetValues(typeof(MediaWikiImageInfoProps)))
            {
                if (iiProps.HasFlag(flag))
                {
                    propsSb.Append(flag.ToString().ToLower()).Append("|");
                }
            }

            // remove the trailing "|" that is now the last char
            propsSb.Remove(propsSb.Length - 1, 1);

            // compose titles argument value
            var titleSb = new StringBuilder();
            foreach (var title in titles)
            {
                titleSb.Append(title).Append("|");
            }

            // remove the trailing "|" that is now the last char
            titleSb.Remove(titleSb.Length - 1, 1);

            var querySb = new StringBuilder()
                .Append(_mediaWikiApiUrl)
                .Append("?action=query")
                .Append("&prop=imageinfo")
                .Append("&iiprop=").Append(propsSb)
                .Append("&format=json")
                .Append("&titles=").Append(titleSb);

            return querySb.ToString();
        }

        /// <summary>
        /// Performs common validation of MediaWiki API response object.
        /// </summary>
        /// <param name="props"></param>
        /// <returns></returns>
        public static bool AssertImageInfoPropsNotNull(ImageInfoRootObject? imageInfo, MediaWikiImageInfoProps props)
        {
            // Assert imageInfo has the "pages" child.
            if (imageInfo?.query?.pages == null)
            {
                return false;
            }

            // Every page has to have a title.
            if (imageInfo.query.pages.Any(x => x.Value?.title == null))
            {
                return false;
            }

            /// Using Dictionary makes this implementation resilient to new enums being added to <see cref="MediaWikiImageInfoProps"/>.
            /// It will throw a KeyNotFoundException on an unknown flag rather than silently pass.
            /// Keep it this way if modifying.
            Dictionary<MediaWikiImageInfoProps, Func<KeyValuePair<int, Page>?, bool>> validator = new()
            {
                [MediaWikiImageInfoProps.None] = page => true,
                [MediaWikiImageInfoProps.ExtMetadata] = page => page?.Value?.imageinfo?.SingleOrDefault()?.extmetadata != null,
                [MediaWikiImageInfoProps.Url] = page => page?.Value?.imageinfo?.SingleOrDefault()?.url != null && page?.Value?.imageinfo?.SingleOrDefault()?.descriptionurl != null,
            };

            // Early exit if any of the enum flags don't pass its validator.
            foreach (MediaWikiImageInfoProps flag in Enum.GetValues(typeof(MediaWikiImageInfoProps)))
            {
                if (props.HasFlag(flag) && imageInfo.query.pages.Any(page => !validator[flag](page)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
