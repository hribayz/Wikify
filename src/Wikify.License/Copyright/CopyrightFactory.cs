using System;
using Wikify.Common.License;

namespace Wikify.License.Copyright
{
    public class CopyrightFactory : ICopyrightFactory
    {
        private ICopyrightResolver _copyrightResolver;
        public CopyrightFactory(ICopyrightResolver copyrightResolver)
        {
            _copyrightResolver = copyrightResolver;
        }
        public ICopyright CreateCopyright(CopyrightLicense copyrightLicense)
        {
            var isCopyrighted = _copyrightResolver.IsCopyrighted(copyrightLicense);
            var isAttributionRequired = _copyrightResolver.IsAttributionRequired(copyrightLicense);

            return new Copyright(
                isCopyrighted: isCopyrighted,
                copyrightLicense: copyrightLicense,
                isAttributionRequired: isAttributionRequired);
        }

        public IAttribution CreateAttribution(string title, string author, string credit)
        {
            return new Attribution(title, author, credit);
        }
        public CopyrightLicense ParseLicense(string license)
        {
            throw new NotImplementedException();
        }
    }
}
