using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Display
{
    public abstract class ADisplayConfiguration
    {
        public ImageResolution Resolution { get; protected set; }
        public static ADisplayConfiguration None { get; protected set; }
    }
}
