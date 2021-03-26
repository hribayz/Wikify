using System.Collections.Generic;
using Wikify.Common.Content.Types;
using Wikify.Common.Id;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    public class ArticleElement : IElement<WikiArticle>
    {
        private IEnumerable<IElement> _children;
        private WikiArticle _content;
        private IIdentifier<WikiArticle> _identifier;
        private ILicense _license;

        public ArticleElement(IEnumerable<IElement> children, WikiArticle content, IIdentifier<WikiArticle> identifier, ILicense license)
        {
            _children = children;
            _content = content;
            _identifier = identifier;
            _license = license;
        }

        public IEnumerable<IElement> GetChildren() => _children;

        public WikiArticle GetContent() => _content;

        public IIdentifier GetIdentifier() => _identifier;

        public ILicense GetLicense() => _license;
    }
}
