namespace Wikify.Common.License
{
    public interface ICopyright
    {
        public bool IsCopyrighted { get; }
        public CopyrightLicense CopyrightLicense { get; }
        public bool IsAttributionRequired { get; }
    }
}
