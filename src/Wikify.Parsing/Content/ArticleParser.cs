using AngleSharp;
using System;
using System.IO;
using System.Threading.Tasks;
using Wikify.Common.Content;
using Wikify.Common.Content.Types;

namespace Wikify.Parsing.Content
{
    public class ArticleParser : IContentParser<WikiArticle>
    {
        private IContentParserFactory _contentParserFactory;
        public ArticleParser(IContentParserFactory contentParserFactory)
        {
            _contentParserFactory = contentParserFactory;
        }

        public async Task<WikiArticle> ParseContentAsync(Stream contentStream)
        {
            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(x => x.Content(contentStream));
        }

    }

}
