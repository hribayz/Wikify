using System.Drawing;
using Wikify.Common.Content;

namespace Wikify.Display
{
    interface IImageGeneratorConfigurable : IImageGenerator
    {
        System.Drawing.Image CreateArticleImage(IWikiContainer<WikiArticle> wikiArticle, ADisplayConfiguration displayConfiguration);
    }
}
