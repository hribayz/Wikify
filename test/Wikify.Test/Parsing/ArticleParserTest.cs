﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wikify.Archive;
using Wikify.Archive.AngleSharp;
using Wikify.Common.Id;
using Wikify.Parsing.Content;

namespace Wikify.Test.Parsing
{
    [TestClass]
    public class WikiDownloaderTest
    {
        private static ArticleDownloader _articleDownloader;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _articleDownloader = new ArticleDownloader(
                (new LoggerFactory()).CreateLogger<ArticleDownloader>(),
                new Common.Network.NetworkingProvider());
        }

        [TestMethod]
        [DataRow("C:\\Users\\TK\\Desktop\\Laboratori Nazionali del Gran Sasso - Wikipedia.html")]
        public async Task TestParseArticle(string articleFileName)
        {
            var html = File.ReadAllText(articleFileName);
            await _articleDownloader.GetElementAsync(new ArticleIdentifier(articleFileName));

            ;
        }

    }
}
