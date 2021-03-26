using System.Drawing;

namespace Wikify.Common.Content.Types
{
    public class WikiArticle : AWikiContent
    {
        public string Title { get; private set; }

        public WikiArticle(string title)
        {
            Title = title;
        }
    }
}
