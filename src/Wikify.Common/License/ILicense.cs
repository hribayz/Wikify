using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Common.License
{
    public interface ILicense
    {
        public ICopyright Copyright { get; }
        public IAttribution Attribution { get; }
        public LicenseRestrictionsEnum Restrictions { get; }
    }
}
