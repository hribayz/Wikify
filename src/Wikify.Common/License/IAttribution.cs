using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Common.License
{
    public interface IAttribution
    {
        public string Title { get; }
        public string Author { get; }
        public string MediaSource { get; }
        public string License { get; }
        public string LicenseSource { get; }
    }
}
