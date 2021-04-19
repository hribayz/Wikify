using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Common.License
{
    public interface IAttribution
    {
        public string ArtTitle { get; }
        public string Artist { get; }
        public string Credit { get; }
        public string ArtDescription { get; }
        public string AttributonText { get; }

    }
}
