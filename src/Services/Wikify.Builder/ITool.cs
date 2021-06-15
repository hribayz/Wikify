using System;
using System.Threading.Tasks;

namespace Wikify.Builder
{

    public interface ITool
    {
        public Task<IState> ApplyAsync(IState state);
    }
}
