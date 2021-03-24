using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Common.License
{
    public interface IWikiObjectLicense
    {
        public IEnumerable<IWikiObjectLicenseAttribution> GetAttributions();
    }
    public interface IWikiObjectLicenseAttribution
    {

    }
}
