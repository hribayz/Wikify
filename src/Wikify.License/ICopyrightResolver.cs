using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.License
{
    public interface ICopyrightResolver
    {
        public bool IsCopyrighted(CopyrightLicense copyrightLicense);
        public bool IsAttributionRequired(CopyrightLicense copyrightLicense);
        public bool IsCompatible(CopyrightLicense copyrightLicense, CopyrightLicense withCopyrightLicense);
        public IEnumerable<CopyrightLicense> GetCompatibleWith(CopyrightLicense copyrightLicense);
    }
}
