using System.Drawing;
using System.Threading.Tasks;
using Wikify.Common;
using Wikify.Common.Content;
using Wikify.Common.Content.Types;
using Wikify.Common.Id;

namespace Wikify.Archive
{
    public interface IArchive<T> where T : AWikiContent
    {
        public Task<IContainer<T>> GetElementAsync(IIdentifier<T> elementIdentifier);
        public Task<IContainer<T>> GetElementAsync(IIdentifier<T> elementIdentifier, RetrieveOptions retrieveOptions);
    }
}