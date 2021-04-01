using Wikify.Common;
using Wikify.Common.Content.Types;
using Wikify.Common.Id;

namespace Wikify.Common.Id
{
    internal class ImageIdentifier : IIdentifier<WikiImage>
    {
        private string _url;
        public ImageIdentifier(string url)
        {
            _url = url;
        }

        public string GetFileName()
        {
            throw new System.NotImplementedException();
        }

        public string GetUrl() => _url;
    }
}
