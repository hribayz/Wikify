using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Archive
{
    public class RetrieveOptions
    {
        public bool Mobile { get; private set; } = false;

        public static RetrieveOptions Default { get; set; } = new RetrieveOptions(false);
        public RetrieveOptions(bool mobile)
        {
            Mobile = mobile;
        }

    }

}
