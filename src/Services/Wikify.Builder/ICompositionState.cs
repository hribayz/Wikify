namespace Wikify.Builder
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICompositionState
    {
        public bool IsDefault { get; }
        public ITool LastToolApplied { get; }
        public string GetContentState(string content);
    }
}
