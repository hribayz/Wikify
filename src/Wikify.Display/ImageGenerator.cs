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
    public class ImageGenerator : IImageGenerator
    {
        public ImageGenerator()
        {

        }

        public Image CreateArticleImage(IContainer<WikiArticle> wikiArticle)
        {
            throw new NotImplementedException();
        }

    }
}
