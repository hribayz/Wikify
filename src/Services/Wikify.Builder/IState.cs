namespace Wikify.Builder
{
    public interface IState
    {
        public bool IsDefault { get; }
        public ITool LastToolApplied { get; }
    }
}
