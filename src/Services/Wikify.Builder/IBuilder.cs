using System.Threading.Tasks;
using Wikify.Common.Content.Parsed;

namespace Wikify.Builder
{
    public interface IBuilder
    {
        public Task<IWikiComponent> BuildAsync();
        public IState CurrentState { get; }
        public Task<IState> UndoAsync();
        public Task<IState> ApplyToolAsync(ITool tool);

    }
}
