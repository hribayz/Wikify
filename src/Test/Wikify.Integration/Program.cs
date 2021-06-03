using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Threading.Tasks;
using Wikify.Archive;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Parsing.Content;
using Wikify.Test;

namespace Wikify.Integration
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var client = new MwParserClient();
            await client.TestArticleHasInfoPanelAsync("Edinburgh");

            ;
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

        public async Task TestArticleHasInfoPanelAsync(string title)
        {
            var articleContainer = await GetArticleContainerAsync(title);
            var children = articleContainer.GetChildren();
            var infoPanels = articleContainer.GetChildren(x => x.ComponentType == WikiComponentType.InfoPanel);

            // Let background logger finish its job before hitting breakpoint.
            await Task.Delay(1000);

            ;
        }
    }
}
