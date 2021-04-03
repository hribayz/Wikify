using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common;
using Wikify.Common.Content;

namespace Wikify.Display
{
    interface IImageGenerator
    {
        System.Drawing.Image CreateArticleImage(IWikiContainer<WikiArticle> wikiArticle);
    }
}
