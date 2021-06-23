using System;
using System.Threading.Tasks;

namespace Wikify.Builder
{

    public interface ITool
    {
        public Task<ICompositionState> ApplyAsync(ICompositionState state);
        public bool Equals(object? obj);
        public int GetHashCode();
    }
    public class Tool : ITool
    {
        public Task<ICompositionState> ApplyAsync(ICompositionState state)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
