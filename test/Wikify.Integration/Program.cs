using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
//using Serilog;
using System;
using Wikify.Archive;
using Wikify.Common.Id;
using Wikify.Parsing.Content;
using Wikify.Test;

namespace Wikify.Integration
{
    public class Program
    {
        static void Main(string[] args)
        {
            MwParserClient client = new MwParserClient();
            client.TestLogging();
        }
    }

    public class MwParserClient : WikifyTestBase
    {
        private IArticleIdentifierFactory _articleIdentifierFactory;
        private IArticleArchive _articleDownloader;
        private IArticleParser _articleParser;

        public MwParserClient()
        {
            _articleIdentifierFactory = GetService<IArticleIdentifierFactory>();
            _articleDownloader = GetService<IArticleArchive>();
            _articleParser = GetService<IArticleParser>();
        }

        private async Task<IWikiContainer<IWikiArticle>> GetArticleContainerAsync(string title)
        {
            var articleArchive = await _articleDownloader.GetArticleAsync(_articleIdentifierFactory.GetIdentifier(title, Common.LanguageEnum.English), TextContentModel.WikiText);
            return await _articleParser.GetContainerAsync(articleArchive);
        }
    }
}
