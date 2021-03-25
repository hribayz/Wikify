using System.Drawing;
using Wikify.Common.Content;
using Wikify.Common.Content.Types;

namespace Wikify.Display
{
    interface IImageGeneratorConfigurable : IImageGenerator
    {
        Image CreateArticleImage(IElement<ArticleContent> wikiArticle, ADisplayConfiguration displayConfiguration);
    }
}
