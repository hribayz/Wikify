using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Id;

namespace Wikify.Archive
{
    public interface IArchive<T> where T : IWikiMedia
    {
        public Task<T> GetMediaAsync(IIdentifier<T> elementIdentifier);
    }
}