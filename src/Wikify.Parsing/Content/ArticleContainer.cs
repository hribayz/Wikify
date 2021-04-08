using System.Collections.Generic;
using Wikify.Common.Content;

namespace Wikify.Parsing.Content
{
    public class ArticleContainer : IWikiContainer<IWikiArticle>
    {
        public IWikiArticle Content { get; private set; }
        public IEnumerable<IWikiComponent> Children => throw new System.NotImplementedException();
        public WikiComponentType ComponentType => throw new System.NotImplementedException();

        public ArticleContainer(IWikiArticle content)
        {
            Content = content;
        }
    }
}
