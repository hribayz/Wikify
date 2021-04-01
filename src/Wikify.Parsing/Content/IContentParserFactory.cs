using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content.Types;

namespace Wikify.Parsing.Content
{
    public interface IContentParserFactory
    {
        public IStreamedContentParser<T> CreateStreamedContentParser<T>() where T : AWikiContent;
    }
}
