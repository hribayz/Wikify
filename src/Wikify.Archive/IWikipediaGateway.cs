using System;

namespace Wikify.Archive
{
    public interface IWikipediaGateway
    {
        public string GetArticle(Uri articleUri);
        public string GetArticle(string articleTitle);
    }
}
