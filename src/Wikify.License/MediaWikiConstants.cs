using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.License
{
    public interface IMediaWikiConstantsContainer
    {
        public HashSet<string> CopyrightKeys { get; }
        public HashSet<string> RestrictionsKeys { get; }
    }
    public class MediaWikiConstantsContainer : IMediaWikiConstantsContainer
    {
        private readonly HashSet<string> _copyrightKeys = new()
        {
            "License",
            "LicenseShortName"
        };

        private readonly HashSet<string> _restrictionsKeys = new()
        {
            "Categories",
            "Restrictions"
        };
        public HashSet<string> CopyrightKeys => _copyrightKeys;
        public HashSet<string> RestrictionsKeys => _restrictionsKeys;
    }
}
