using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Wikify.Archive;
using Wikify.Common.Content;
using Wikify.Common.Id;
using Wikify.Parser.Content;
using Wikify.Test;

namespace Wikify.Integration
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var client = new ClientMock();
            var articleContainer = await client.GetArticleContainerAsync("Edinburgh");

            //Expression
        }
    }

    public class ClientMock : WikifyTestBase { }

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
