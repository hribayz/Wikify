using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content;

namespace Wikify.Parsing.Content
{
    public interface IWikiContentFactory
    {
        public IWikiComponent CreateComponent(WikiComponentType componentType, IReadOnlyCollection<IWikiComponent> children);
        public IWikiContainer<IWikiArticle> CreateArticle(IWikiArticle article, IReadOnlyCollection<IWikiComponent> children);
        public IWikiContainer<IWikiImage> CreateImage(IWikiImage image, WikiComponentType componentType, IReadOnlyCollection<IWikiComponent> children);
    }
    public class WikiContentFactory : IWikiContentFactory
    {
        public IWikiContainer<IWikiArticle> CreateArticle(IWikiArticle article, IReadOnlyCollection<IWikiComponent> children)
        {
            return new ArticleContainer(article, children);
        }

        public IWikiComponent CreateComponent(WikiComponentType componentType, IReadOnlyCollection<IWikiComponent> children)
        {
            return new WikiComponent(componentType, children);
        }

        public IWikiContainer<IWikiImage> CreateImage(IWikiImage image, WikiComponentType componentType, IReadOnlyCollection<IWikiComponent> children)
        {
            return new ImageContainer(image, componentType, children);
        }
    }
}
