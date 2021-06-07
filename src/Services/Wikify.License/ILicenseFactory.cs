using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Domain;
using Wikify.Common.License;

namespace Wikify.License
{
    public interface ILicenseFactory
    {
        public ILicense CreateLicense(ICopyright copyright, IAttribution attribution, LicenseRestrictionsEnum licenseRestrictions);
    }
}
