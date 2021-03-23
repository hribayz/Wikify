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
        Image CreateArticleImage(AWikiArticle wikiArticle, ADisplayConfiguration displayConfiguration);
    }
    public class WikiImageGenerator : IWikiImageGenerator
    {
        public Image CreateArticleImage(AWikiArticle wikiArticle, ADisplayConfiguration displayConfiguration)
        {
            throw new NotImplementedException();
        }
    }
}
