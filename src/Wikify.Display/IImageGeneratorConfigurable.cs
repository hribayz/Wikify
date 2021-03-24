using System.Drawing;
using Wikify.Common.Element;

namespace Wikify.Display
{
    interface IImageGeneratorConfigurable : IImageGenerator
    {
        Image CreateArticleImage(IArticleElement wikiArticle, ADisplayConfiguration displayConfiguration);
    }
}
