namespace Wikify.Common.Id
{
    public interface IArticleIdentifier : IIdentifier
    {
        public LanguageEnum Language { get; }
    }

}
