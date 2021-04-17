using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.Network;
using Wikify.License;

namespace Wikify.Archive
{
    public class MediaWikiArticleDownloader : DownloaderBase, IArticleArchive
    {
        private IMediaWikiApiAdaptor _mediaWikiApiAdaptor;
        private MediaWikiUtils _utils;
        public MediaWikiArticleDownloader(ILogger logger, INetworkingProvider networkingProvider, ILicenseProvider licenseProvider, IWikiMediaFactory wikiMediaFactory, IMediaWikiApiAdaptor mediaWikiApiAdaptor) :
            base(logger, networkingProvider, licenseProvider, wikiMediaFactory)
        {
            _mediaWikiApiAdaptor = mediaWikiApiAdaptor;
            _utils = new MediaWikiUtils();
        }
        public async Task<IWikiArticle> GetArticleAsync(IArticleIdentifier articleIdentifier, WikiContentModel contentModel)
        {
            try
            {
                var parseQuery = _utils.GetParseQuery(articleIdentifier.Title,articleIdentifier.Language, contentModel);
                var contentTask = _client.GetStringAsync(parseQuery);

                var license = await _licenseProvider.GetLicenseAsync(articleIdentifier);
                var content = await contentTask;

                return _wikiMediaFactory.CreateWikiArticle(articleIdentifier, license, content, contentModel);
            }
            // TODO : try to recover from networking related exceptions by re-instantiating client
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
    }
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
                WikiContentModel.Html => "text",
                WikiContentModel.WikiText => "wikitext",
                WikiContentModel.Stream => "wikitext",
                _ => throw new NotImplementedException()
            };

            // https://en.wikipedia.org/w/api.php?action=parse&page=Article_title&prop={wikitext|text}

            return new StringBuilder()
                .Append(endpoint)
                .Append("&page=").Append(articleTitle)
                .Append("&prop=").Append(prop)
                .ToString();
        }
    }
}
