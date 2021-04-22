using Wikify.Common.License;

namespace Wikify.License
{
    public class Copyright : ICopyright
    {
        public bool IsCopyrighted { get; }
        public CopyrightLicense CopyrightLicense { get; }
        public bool IsAttributionRequired { get; }
        public IAttribution Attribution { get; }

        public Copyright(bool isCopyrighted, CopyrightLicense copyrightLicense, bool isAttributionRequired, IAttribution attribution)
        {
            IsCopyrighted = isCopyrighted;
            CopyrightLicense = copyrightLicense;
            IsAttributionRequired = isAttributionRequired;
            Attribution = attribution;
        }
    }
}
