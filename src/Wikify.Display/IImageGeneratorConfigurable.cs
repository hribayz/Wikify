using System.Drawing;
using Wikify.Common.Content;
using Wikify.Common.Content.Types;

namespace Wikify.Display
{
    interface IImageGeneratorConfigurable : IImageGenerator
    {
        System.Drawing.Image CreateArticleImage(IElement<WikiArticle> wikiArticle, ADisplayConfiguration displayConfiguration);
    }
}
