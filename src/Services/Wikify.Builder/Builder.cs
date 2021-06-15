using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Wikify.Common.Content.Parsed;

namespace Wikify.Builder
{
    public class Builder : IBuilder
    {
        // This implementation is thread-unsafe for performance. Instantiate as scoped.
        private ILogger _logger;
        private Stack<IState> _stateStack;
        private IWikiComponent _rootComponent;

        public IState CurrentState => _stateStack.Peek();

        public Builder(ILogger<Builder> logger, IWikiComponent rootComponent, IState state)
        {
            _logger = logger;
            _rootComponent = rootComponent;

            _stateStack = new Stack<IState>();
            _stateStack.Push(state);
        }

        public async Task<IState> ApplyToolAsync(ITool tool)
        {
            var newState = await tool.ApplyAsync(CurrentState);
            _stateStack.Push(newState);
            return newState;
        }

        public async Task<IWikiComponent> BuildAsync()
        {
            // Traverse the IWikiComponent tree
            // 
        }

        public async Task<IState> UndoAsync()
        {
            if (_stateStack.Count == 1)
            {
                #region Debug assertion

                var state = _stateStack.Peek();
                Debug.Assert(state.IsDefault);

                #endregion

                return CurrentState;
            }

            _stateStack.Pop();
            return _stateStack.Peek();
        }
    }
}
