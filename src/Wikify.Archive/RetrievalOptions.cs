using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Archive
{
    public class RetrievalOptions
    {
        public bool Mobile { get; private set; } = false;

        public static RetrievalOptions Default { get; set; } = new RetrievalOptions(false);
        public RetrievalOptions(bool mobile)
        {
            Mobile = mobile;
        }

    }

}
