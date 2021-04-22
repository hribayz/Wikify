using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.License;

namespace Wikify.License
{
    public interface ICopyrightFactory
    {
        public ICopyright CreateCopyright(CopyrightLicense copyrightLicense, IAttribution attribution);
        public CopyrightLicense ParseLicense(string license);
        public IAttribution CreateAttribution(string title, string author, string source, string license);
        public IAttribution CreateAttribution(string title, string author, string source, string license, string plainText);
    }
    public class CopyrightFactory : ICopyrightFactory
    {
        private ICopyrightResolver _copyrightResolver;
        public CopyrightFactory(ICopyrightResolver copyrightResolver)
        {
            _copyrightResolver = copyrightResolver;
        }
        public ICopyright CreateCopyright(CopyrightLicense copyrightLicense, IAttribution attribution)
        {
            var isCopyrighted = _copyrightResolver.IsCopyrighted(copyrightLicense);
            var isAttributionRequired = _copyrightResolver.IsAttributionRequired(copyrightLicense);

            return new Copyright(
                isCopyrighted: isCopyrighted, 
                copyrightLicense: copyrightLicense,
                isAttributionRequired: isAttributionRequired,
                attribution: attribution);
        }

        public IAttribution CreateAttribution(string title, string author, string source, string license)
        {
            var plainText = new StringBuilder()
                .Append(title)
                .Append(" by ").Append(author)
                .Append(" licensed under ").Append(license).Append(". ");
                
        }

        public IAttribution CreateAttribution(string title, string author, string source, string license, string plainText)
        {
            return new Attribution(title, author, source, license, plainText);
        }

        public CopyrightLicense ParseLicense(string license)
        {
            throw new NotImplementedException();
        }
    }
}
