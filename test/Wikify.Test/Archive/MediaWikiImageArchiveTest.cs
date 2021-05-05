using System.Drawing;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wikify.Archive;
using Wikify.Common;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Common.Network;
using Wikify.License;
using Wikify.License.Copyright;
using Wikify.License.Tokenization;


namespace Wikify.Test.Archive
{
    [TestClass]
    public class MediaWikiImageArchiveTest
    {
        private static ILoggerFactory _loggerFactory;
        private static IImageArchive _imageArchive;
        private static IImageIdentifierFactory _imageIdentifierFactory;
        private static IImageIdProvider _imageIdProvider;
        private static INetworkingProvider _networkingProvider;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _loggerFactory = new LoggerFactory();
            _imageIdentifierFactory = new ImageIdentifierFactory();
            _networkingProvider = new NetworkingProvider(_loggerFactory.CreateLogger<NetworkingProvider>());
            _imageArchive = new MediaWikiImageDownloader(
                _loggerFactory.CreateLogger<MediaWikiImageDownloader>(),
                new WikiMediaFactory(),
                new ImageLicenseProvider(
                    _loggerFactory.CreateLogger<MediaWikiImageDownloader>(),
                    new CopyrightFactory(new CopyrightResolver()),
                    new LicenseFactory(),
                    new CopyrightTokenizer(_loggerFactory.CreateLogger<CopyrightTokenizer>(), new MediaWikiConstantsContainer()),
                    new LicenseRestrictionsTokenizer()),
                    _networkingProvider
                );
            _imageIdProvider = new ImageIdProvider(
                _imageIdentifierFactory,
                _loggerFactory.CreateLogger<ImageIdProvider>(),
                _networkingProvider);
        }


        [TestMethod]
        [DataRow("File:Logo_Adidas.png", 512)]
        public async Task TestDownloadImageAsync(string imageTitle, int expectedHeight)
        {
            var imageId = await _imageIdProvider.GetIdentifierAsync(imageTitle);
            var image = await _imageArchive.GetImageAsync(imageId);

            Assert.AreEqual(expectedHeight, image.Image.Height);
        }
    }
}
