using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common;
using Wikify.Common.Element;

namespace Wikify.Display
{
    interface IImageGenerator
    {
        Image CreateArticleImage(IArticleElement wikiArticle);
    }
    public class ImageGenerator : IImageGeneratorConfigurable
    {
        public Image CreateArticleImage(IArticleElement wikiArticle, ADisplayConfiguration displayConfiguration)
        {
            throw new NotImplementedException();
        }

        public Image CreateArticleImage(IArticleElement wikiArticle)
        {
            throw new NotImplementedException();
        }
    }
}
