using System.Threading.Tasks;
using Wikify.Common.Id;

namespace Wikify.Parsing.Id
{
    /// <summary>
    /// Gathers and validates image metadata for the purpose of image identification. 
    /// </summary>
    public class ImageIdParser : IImageIdParser
    {
        private IImageIdentifierFactory _imageIdentifierFactory;
        public ImageIdParser(IImageIdentifierFactory imageIdentifierFactory)
        {
            _imageIdentifierFactory = imageIdentifierFactory;
        }
        /// <summary>
        /// Provides an identifier object for an image.
        /// </summary>
        /// <param name="imageTitle">File name of the image.</param>
        /// <returns>Image identification object.</returns>
        public async Task<IImageIdentifier> GetIdentifierAsync(string imageTitle)
        {
            
        }
    }
}
