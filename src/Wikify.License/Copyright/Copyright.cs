using Wikify.Common.License;

namespace Wikify.License.Copyright
{
    public class Copyright : ICopyright
    {
        public bool IsCopyrighted { get; }
        public CopyrightLicense CopyrightLicense { get; }
        public bool IsAttributionRequired { get; }

        public Copyright(bool isCopyrighted, CopyrightLicense copyrightLicense, bool isAttributionRequired)
        {
            IsCopyrighted = isCopyrighted;
            CopyrightLicense = copyrightLicense;
            IsAttributionRequired = isAttributionRequired;
        }
    }
}
