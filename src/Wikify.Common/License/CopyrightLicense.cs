using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Common.License
{
    public enum CopyrightLicenseEnum
    {
        PublicDomainEquivalent, 
        CcBy, 
        CcBySa, 
        Gfdl,
        Unknown
    }

    /// --- WARNING ---
    /// Changes to this enum will cause the <see cref="Wikify.License.Copyright.CopyrightResolver"/> current implementation throw not implemented exception.
}
