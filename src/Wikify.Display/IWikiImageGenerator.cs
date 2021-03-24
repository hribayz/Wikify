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
    interface IWikiImageGenerator
    {
        Image CreateArticleImage(IWikiArticleElement wikiArticle);
    }
    interface IWikiImageGeneratorConfigurable : IWikiImageGenerator
    {
        Image CreateArticleImage(IWikiArticleElement wikiArticle, ADisplayConfiguration displayConfiguration);
    }
    public class WikiImageGenerator : IWikiImageGeneratorConfigurable
    {
        public Image CreateArticleImage(IWikiArticleElement wikiArticle, ADisplayConfiguration displayConfiguration)
        {
            throw new NotImplementedException();
        }

        public Image CreateArticleImage(IWikiArticleElement wikiArticle)
        {
            throw new NotImplementedException();
        }
    }
}
