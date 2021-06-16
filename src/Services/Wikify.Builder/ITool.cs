using System;
using System.Threading.Tasks;

namespace Wikify.Builder
{

    public interface ITool
    {
        public Task<IState> ApplyAsync(IState state);
        public bool Equals(object? obj);
        public int GetHashCode();
    }
    public class Tool : ITool
    {
        public Task<IState> ApplyAsync(IState state)
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
