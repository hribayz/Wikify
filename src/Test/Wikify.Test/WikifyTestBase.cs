using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Archive;
using Wikify.Common.Content;
using Wikify.Common.Domain;
using Wikify.Common.Id;
using Wikify.Common.Network;
using Wikify.License;
using Wikify.License.Copyright;
using Wikify.Parser.Content;
using Wikify.Parser.MwParser;
using Wikify.Parser.MwParser.Specifications;

namespace Wikify.Test
{
    public abstract class WikifyTestBase
    {
        private static readonly ServiceProvider _serviceProvider;

        static WikifyTestBase()
        {
            var services = new ServiceCollection();

            services.AddSingleton<INetworkingProvider, NetworkingProvider>();
            services.AddSingleton<IArticleIdentifierFactory, ArticleIdentifierFactory>();
            services.AddSingleton<IArticleArchive, MediaWikiArticleDownloader>();
            services.AddSingleton<IArticleLicenseProvider, ArticleLicenseProvider>();
            services.AddSingleton<ILicenseFactory, LicenseFactory>();
            services.AddSingleton<ICopyrightResolver, CopyrightResolver>();
            services.AddSingleton<ICopyrightFactory, CopyrightFactory>();
            services.AddSingleton<IWikiMediaFactory, WikiMediaFactory>();
            services.AddSingleton<IAstParser, AstParser>();
            services.AddSingleton<IWikiContentFactory, WikiContentFactory>();
            services.AddSingleton<IPatternMatchingService, PatternMatchingService>();
            services.AddSingleton<IMwParserApi, MwParserApi>();
            services.AddSingleton<ISpecificationProvider, SpecificationProvider>();

            services.AddScoped<IArticleParser, ArticleParser>();

            services.AddLogging(config => config.SetMinimumLevel(LogLevel.Debug).AddConsole());

            _serviceProvider = services.BuildServiceProvider();
            ;
        }

        public async Task<IWikiContainer<IWikiArticle>> GetArticleContainerAsync(string title)
        {
            var articleDownloader = GetService<IArticleArchive>();
            var articleIdentifierFactory = GetService<IArticleIdentifierFactory>();
            var articleParser = GetService<IArticleParser>();

            var articleArchive = await articleDownloader.GetArticleAsync(articleIdentifierFactory.GetIdentifier(title, LanguageEnum.English), TextContentModel.WikiText);
            return await articleParser.GetContainerAsync(articleArchive);
        }

        public T GetService<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
