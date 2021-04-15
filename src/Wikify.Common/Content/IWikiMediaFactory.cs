using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    /// <summary>
    /// Provides the only way of creating instances of IWikiMedia descendants (e.g. WikiImages, WikiArticles). Enforces the golden rule that every instance have the identifier, the license and the content set at creation.
    /// </summary>
    public interface IWikiMediaFactory
    {
        public IWikiArticle CreateWikiArticle(IArticleIdentifier identifier, ILicense license, string content, WikiContentModel contentModel);
        public IWikiImage CreateWikiImage(IImageIdentifier identifier, ILicense license, Image image);
    }
}
