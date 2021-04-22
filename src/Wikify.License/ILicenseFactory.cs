using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.License;

namespace Wikify.License
{
    public interface ILicenseFactory
    {
        public ILicense CreateLicense(CopyrightLicense copyrightLicense, LicenseRestrictionsEnum licenseRestrictions);
    }
}
