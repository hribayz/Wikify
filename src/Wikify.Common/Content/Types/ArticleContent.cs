using System.Drawing;

namespace Wikify.Common.Content.Types
{
    public class ArticleContent : AWikiContent
    {
        public string Title { get; private set; }

        public ArticleContent(string title)
        {
            Title = title;
        }
    }
}
