using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.MediaWikiModels;
using Wikify.Common.Network;
using Wikify.License;
using static Wikify.Common.MediaWikiModels.MediaWikiImageInfoResponse;

namespace Wikify.Archive
{
    public class MediaWikiImageDownloader : IImageArchive
    {
        private readonly ILogger _logger;
        private readonly IWikiMediaFactory _wikiMediaFactory;
        private readonly IImageLicenseProvider _imageLicenseProvider;
        private readonly INetworkingProvider _networkingProvider;

        public MediaWikiImageDownloader(ILogger logger, IWikiMediaFactory wikiMediaFactory, IImageLicenseProvider imageLicenseProvider, INetworkingProvider networkingProvider)
        {
            _logger = logger;
            _wikiMediaFactory = wikiMediaFactory;
            _imageLicenseProvider = imageLicenseProvider;
            _networkingProvider = networkingProvider;
        }

        public async Task<IWikiImage> GetImageAsync(IImageIdentifier imageIdentifier)
        {
            var imageAddress = imageIdentifier.ImageUri;
            var imageAddressUri = new Uri(imageAddress);

            // Image download task on the background.
            var imageTask = _networkingProvider.GetResponseContentStreamAsync(imageAddressUri);

            // Tokenize license in the meantime.
            var license = await _imageLicenseProvider.GetImageLicenseAsync(imageIdentifier);

            var image = Image.FromStream(await imageTask);

            return _wikiMediaFactory.CreateWikiImage(imageIdentifier, license, image);
        }

        public async Task<IReadOnlyCollection<IWikiImage>> GetImagesAsync(IEnumerable<IImageIdentifier> imageIdentifiers)
        {
            List<Task<IWikiImage>> imageTasks = new();

            foreach (var imageIdentifier in imageIdentifiers)
            {
                imageTasks.Add(GetImageAsync(imageIdentifier));
            }

            return await Task.WhenAll(imageTasks);
        }
    }
    public class MediaWikiArticleDownloader : IArticleArchive
    {
        private readonly ILogger _logger;
        private readonly IWikiMediaFactory _wikiMediaFactory;
        private readonly IArticleLicenseProvider _articleLicenseProvider;
        private readonly INetworkingProvider _networkingProvider;

        public MediaWikiArticleDownloader(ILogger logger, INetworkingProvider networkingProvider, IArticleLicenseProvider articleLicenseProvider, IWikiMediaFactory wikiMediaFactory)
        {
            _logger = logger;
            _articleLicenseProvider = articleLicenseProvider;
            _wikiMediaFactory = wikiMediaFactory;
            _networkingProvider = networkingProvider;
        }

        public async Task<IWikiArticle> GetArticleAsync(IArticleIdentifier articleIdentifier, TextContentModel contentModel)
        {
            try
            {
                // Resolve license on a background task, retrieve after content has been parsed.
                var licenseTask = _articleLicenseProvider.GetArticleLicenseAsync(articleIdentifier);

                var parseQuery = MediaWikiUtils.GetParseQuery(articleIdentifier.Title, articleIdentifier.Language, contentModel);
                var parseQueryUri = new Uri(parseQuery);

                var mwResponse = await _networkingProvider.GetResponseContentAsync(parseQueryUri);

                var mwResponseObject = JsonConvert.DeserializeObject<MediaWikiParserResponse.ParserRootObject>(mwResponse);

                string? content = contentModel switch
                {
                    TextContentModel.Text => mwResponseObject?.parse?.text?["*"],
                    TextContentModel.WikiText => mwResponseObject?.parse?.wikitext?["*"],
                    _ => throw new NotImplementedException()
                };

                if (content == null)
                {
                    var logSb = new StringBuilder().Append("Failed to deserialize MediaWiki parser output.").Append(Environment.NewLine)
                        .Append("Media Wiki response content: ").Append(mwResponse);

                    throw new ApplicationException(logSb.ToString());
                }

                // retrieve license from background task
                var license = await licenseTask;

                return _wikiMediaFactory.CreateWikiArticle(articleIdentifier, license, content, contentModel);
            }

            catch (Exception e)
            {
                _logger.LogError(e, "Failed to retrieve article.");
                throw;
            }
        }
    }
}
