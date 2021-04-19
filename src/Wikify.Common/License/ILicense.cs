using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Common.License
{
    public interface ILicense
    {
        public bool IsCopyrighted { get; }
        public bool IsAttributionRequered { get; }
        public IAttribution Attribution { get; }
        public Copyright Copyright { get; }
        public LicenseWarnings Warnings { get; }
        public bool IsCompatibleWith(Copyright copyright);
    }
}
