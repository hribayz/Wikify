using System.IO;
using System.Threading.Tasks;
using Wikify.Common;
using Wikify.Common.Content;
using Wikify.Common.Content.Types;

namespace Wikify.Parsing.Content
{
    public interface IContentParser<T> where T : AWikiContent
    {
        // TODO: isn't it too restrictive to require content be presented as stream?
        public Task<T> ParseContentAsync(Stream contentStream);
    }

}
