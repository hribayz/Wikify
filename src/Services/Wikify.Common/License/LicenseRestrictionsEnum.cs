using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Common.License
{
    [Flags]
    public enum LicenseRestrictionsEnum
    {
        None = 0,
        Personality = 1,
        Trademark = 2,
        Geographical = 4,
        Indigenious = 8,
        Related = 16
    }
}
