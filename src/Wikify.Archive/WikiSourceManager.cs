using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Archive
{
    public interface IElementValidator
    {
        public bool IsValidElement(string tagName, string? id, IEnumerable<string>? classes, string? innerText);
    }
    /// <summary>
    /// This implementation has all blocking operations intentionally placed in the constructor. 
    /// This includes mainly reading the known wiki elements JSON from disk and building the in-memory lookup data structure.
    /// Instantiate this service at startup and asynchronously.
    /// </summary>
    public class WhitelistValidator : IElementValidator
    {
        private HashSet<string> _idWhitelist;
        private HashSet<string> _tagWhitelist;
        public WhitelistValidator()
        {
            _idWhitelist = new(File.ReadAllText("whitelist_id.csv").Split(","));
            _tagWhitelist = new(File.ReadAllText("whitelist_tag.csv").Split(","));
        }

        public bool IsValidElement(string tagName, string? id, IEnumerable<string>? classes, string? innerText)
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
