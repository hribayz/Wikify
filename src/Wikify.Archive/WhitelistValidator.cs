using System.Collections.Generic;
using System.IO;

namespace Wikify.Archive
{
    /// <summary>
    /// This implementation has all blocking operations intentionally placed in the constructor. 
    /// This includes mainly reading the known wiki elements JSON from disk and building the in-memory lookup data structure.
    /// Instantiate this service at startup and asynchronously.
    /// </summary>
    public class WhitelistValidator
    {
        private HashSet<string> _idWhitelist;
        private HashSet<string> _tagWhitelist;
        public WhitelistValidator()
        {
            _idWhitelist = new(File.ReadAllText("whitelist_id.csv").Split(","));
            _tagWhitelist = new(File.ReadAllText("whitelist_tag.csv").Split(","));
        }

        public bool IsValidElement(string tagName, string? id, IEnumerable<string>? classes)
        {
            bool isWhitelisted =
                _tagWhitelist.Contains(tagName) ||
                id != null && _idWhitelist.Contains(id);

            if (isWhitelisted)
            {
                return true;
            }

            return false;
        }
    }
}
