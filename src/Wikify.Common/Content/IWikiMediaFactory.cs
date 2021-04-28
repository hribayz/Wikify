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
    /// Provides methods for creating instances of <see cref="IWikiMedia"/> descendants (e.g. <see cref="IWikiImage"/>, <see cref="IWikiArticle"/>).
    /// Ensures the <see cref="IIdentifier"/>, the <see cref="ILicense"/> and the content references are set at creation.
    /// </summary>
    public interface IWikiMediaFactory
    {
        public IWikiArticle CreateWikiArticle(IArticleIdentifier identifier, ILicense license, string content, TextContentModel contentModel);
        public IWikiImage CreateWikiImage(IImageIdentifier identifier, ILicense license, Image image);
    }
}
