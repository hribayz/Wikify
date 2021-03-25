using System.Drawing;
using System.Threading.Tasks;
using Wikify.Common;
using Wikify.Common.Content;
using Wikify.Common.Content.Types;
using Wikify.Common.Id;

namespace Wikify.Archive
{
    //public interface IArchive
    //{
    //    public Task<IElement> GetElementAsync(IElementIdentifier elementIdentifier);

    //}
    public interface IArchive<T> where T : AWikiContent
    {
        public Task<IElement<T>> GetElementAsync(IElementIdentifier<T> elementIdentifier);
    }
}