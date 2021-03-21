using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common;

namespace Wikify.Display
{
    interface IWikiImageGenerator
    {
        Image GetWikiArticleImage(AWikiArticle wikiArticle, ADisplayConfiguration displayConfiguration);
    }
}
