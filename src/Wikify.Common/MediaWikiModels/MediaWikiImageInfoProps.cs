using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Common.MediaWikiModels
{
    [Flags]
    public enum MediaWikiImageInfoProps
    {
        None = 1,
        ExtMetadata = 2,
        Url = 4,
        DescriptionUrl = 8,

        /// --- WARNING ---
        /// 
        /// Do not modify this enum unless you review and update <see cref="Wikify.Common.MediaWikiUtils.AssertImageInfoPropsNotNull"/>.
        /// The assert will silently OK new enums even if they're invalid.
    }
}
