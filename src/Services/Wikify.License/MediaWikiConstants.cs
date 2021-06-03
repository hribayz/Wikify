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
        public HashSet<string> PublicDomainEquivalentNames { get; }
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

        private readonly HashSet<string> _publicDomainEquivalentNames = new()
        {
            "public domain",
            "pd",
            "cc0",
            "pdm owner"
        };
        public HashSet<string> CopyrightKeys => _copyrightKeys;
        public HashSet<string> RestrictionsKeys => _restrictionsKeys;
        public HashSet<string> PublicDomainEquivalentNames => _publicDomainEquivalentNames;
    }
}
