using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.License;

namespace Wikify.License
{
    internal class License : ILicense
    {
        public ICopyright Copyright { get; }
        public IAttribution Attribution { get; }
        public LicenseRestrictionsEnum Restrictions { get; }

        public License(ICopyright copyright, IAttribution attribution, LicenseRestrictionsEnum licenseRestrictions)
        {
            Copyright = copyright;
            Attribution = attribution;
            Restrictions = licenseRestrictions;
        }
    }
}
