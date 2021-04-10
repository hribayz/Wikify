using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common;
using Wikify.Common.Content;

namespace Wikify.Display
{
    public interface IHtmlRenderer
    {
        Image RenderHtml(DirectoryInfo directory, RenderOptions renderOptions);
    }
}
