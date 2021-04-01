using System;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Content.Types;
using Wikify.Common.Id;

namespace Wikify.Archive.AngleSharp
{
    public class ImageDownloader : IArchive<WikiImage>
    {
        public Task<IContainer<WikiImage>> GetElementAsync(IIdentifier<WikiImage> elementIdentifier)
        {
            throw new NotImplementedException();
        }

        public Task<IContainer<WikiImage>> GetElementAsync(IIdentifier<WikiImage> elementIdentifier, RetrieveOptions retrieveOptions)
        {
            throw new NotImplementedException();
        }
    }
}
