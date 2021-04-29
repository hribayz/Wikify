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
        private readonly IImageIdentifierFactory _imageIdentifierFactory;
        private readonly IImageLicenseProvider _imageLicenseProvider;
        private readonly INetworkingProvider _networkingProvider;

        public MediaWikiImageDownloader(ILogger logger, IWikiMediaFactory wikiMediaFactory, IImageIdentifierFactory imageIdentifierFactory, IImageLicenseProvider imageLicenseProvider, INetworkingProvider networkingProvider)
        {
            _logger = logger;
            _wikiMediaFactory = wikiMediaFactory;
            _imageIdentifierFactory = imageIdentifierFactory;
            _imageLicenseProvider = imageLicenseProvider;
            _networkingProvider = networkingProvider;
        }

        public async Task<IWikiImage> GetImageAsync(IImageIdentifier imageIdentifier)
        {
            var imageMetadataQuery = MediaWikiUtils.GetImageMetadataQuery(new[] { imageIdentifier.Title }, new[] { "extmetadata", "url" });
            var imageMetadataQueryUri = new Uri(imageMetadataQuery);

            var iiResponse = await _networkingProvider.GetResponseContentAsync(imageMetadataQueryUri);
            _logger.LogDebug("iiResponse: " + iiResponse);

            var iiResponseObject = JsonConvert.DeserializeObject<ImageInfoRootObject>(iiResponse) ?? throw new ApplicationException("iiResponse was deserialized into null. ");
            Page imagePage = iiResponseObject.query.pages.Single().Value;
            Imageinfo imageinfo = imagePage.imageinfo.Single();

            var imageAddress = imageinfo.url;
            var imageAddressUri = new Uri(imageAddress);

            var imageTask = _networkingProvider.GetResponseContentStreamAsync(imageAddressUri);

            var licenseTask = _imageLicenseProvider.GetImageLicenseAsync(
                _imageIdentifierFactory.GetIdentifier(imageIdentifier, imageinfo.extmetadata.ToDictionary(x => x.Key, x => x.Value.value)));

            var image = Image.FromStream(await imageTask);

            var license = await licenseTask;

            return _wikiMediaFactory.CreateWikiImage(imageIdentifier, license, image);
        }

        public Task<IReadOnlyCollection<IWikiImage>> GetImagesAsync(IEnumerable<IImageIdentifier> imageIdentifiers)
        {
            throw new NotImplementedException();
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
