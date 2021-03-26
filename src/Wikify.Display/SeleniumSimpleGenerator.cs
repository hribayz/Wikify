using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Content.Types;

namespace Wikify.Display
{
    public class SeleniumSimpleGenerator : IImageGenerator
    {
        public System.Drawing.Image CreateArticleImage(IElement<WikiArticle> wikiArticle)
        {
            throw new NotImplementedException();
        }
    }
}
