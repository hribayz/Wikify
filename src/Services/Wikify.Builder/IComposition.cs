using System.Threading.Tasks;
using Wikify.Common.Content.Parsed;

namespace Wikify.Builder
{
    public interface IComposition
    {
        public Task<IWikiComponent> BuildAsync();
        public Task<ICompositionState> UndoAsync();
        public Task<ICompositionState> UndoToolAsync(ITool tool);
        public Task<ICompositionState> ApplyToolAsync(ITool tool);

    }
}
