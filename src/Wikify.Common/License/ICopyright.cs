namespace Wikify.Common.License
{
    public interface ICopyright
    {
        public bool IsCopyrighted { get; }
        public CopyrightLicenseEnum CopyrightLicense { get; }
        public bool IsAttributionRequired { get; }
    }
}
