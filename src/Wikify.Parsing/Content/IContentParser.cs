using System.IO;
using System.Threading.Tasks;
using Wikify.Common;
using Wikify.Common.Content;
using Wikify.Common.Content.Types;

namespace Wikify.Parsing.Content
{
    public interface IStreamedContentParser<T> where T : AWikiContent
    {
        public Task<T> ParseContentAsync(Stream contentStream);
    }

}
