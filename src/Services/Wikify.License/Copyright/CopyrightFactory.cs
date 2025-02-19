﻿using System;
using Wikify.Common.License;

namespace Wikify.License.Copyright
{
    public class CopyrightFactory : ICopyrightFactory
    {
        private const string _authorUnknown = "Unknown author";
        private ICopyrightResolver _copyrightResolver;
        public CopyrightFactory(ICopyrightResolver copyrightResolver)
        {
            _copyrightResolver = copyrightResolver;
        }
        public ICopyright CreateCopyright(CopyrightLicenseEnum copyrightLicense)
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
        public IAttribution CreateAttributionWithoutAuthor(string title, string credit)
        {
            return new Attribution(title, _authorUnknown, credit);
        }
    }
}
