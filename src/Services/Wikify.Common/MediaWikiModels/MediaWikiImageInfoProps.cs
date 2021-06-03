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

        /// --- WARNING ---
        /// 
        /// 1.  Do not modify this enum unless you review and update <see cref="MediaWikiUtils.AssertImageInfoPropsNotNull"/>.
        ///     The assert may silently OK unknown enums even if they're invalid.
        /// 2.  The only acceptable variation from the actual mediawiki iiprops is case.
        ///     If you want to deviate from this, introduce a translation service and fix all .ToString().ToLower() usages
    }
}
