using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content;

namespace Wikify.Parsing.Content
{
    /// <summary>
    /// Analyzes the content of an <see cref="IWikiArticle"/> and provides methods to interact with its structure.
    /// </summary>
    public interface IArticleParser
    {
        /// <summary>
        /// Provides the composition of an analyzed article as a reference to the root of its tree.
        /// </summary>
        /// <param name="wikiArticle">Component with content to parse.</param>
        /// <returns>Root component of the article composition.</returns>
        public Task<IWikiContainer<IWikiArticle>> GetContainerAsync(IWikiArticle wikiArticle);
    }
}
