using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common;
using Wikify.Common.Content;

namespace Wikify.Archive
{
    public interface IMediaWikiApiAdaptor
    {
        public string GetEndpoint(LanguageEnum language, WikiContentModel contentModel);
    }
}
