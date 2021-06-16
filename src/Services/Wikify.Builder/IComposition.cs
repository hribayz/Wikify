using System.Threading.Tasks;
using Wikify.Common.Content.Parsed;

namespace Wikify.Builder
{
    public interface IComposition
    {
        public Task<IWikiComponent> BuildAsync();
        public Task<IState> UndoAsync();
        public Task<IState> UndoToolAsync(ITool tool);
        public Task<IState> ApplyToolAsync(ITool tool);

    }
}
