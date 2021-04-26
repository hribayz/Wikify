using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Wikify.Common.License;

namespace Wikify.License.Tokenization
{
    public interface ITokenizer
    {
        public CopyrightLicenseEnum GetCopyrightLicense(IEnumerable<KeyValuePair<string, string>> attributes);
        public LicenseRestrictionsEnum GetLicenseRestrictions(IEnumerable<KeyValuePair<string, string>> attributes);
    }
    public class Tokenizer : ITokenizer
    {
        private readonly HashSet<string> _publicDomainEquivalentNames = new()
        {
            "public domain",
            "pd",
            "cc0",
            "pdm owner"
        };
        private readonly Dictionary<string, CopyrightLicenseEnum> _copyrightLicenses = new()
        {
            ["cc by sa 1.0"] = CopyrightLicenseEnum.CcBySa10,
            ["cc by sa 2.0"] = CopyrightLicenseEnum.CcBySa20,
            ["cc by sa 2.5"] = CopyrightLicenseEnum.CcBySa25,
            ["cc by sa 3.0"] = CopyrightLicenseEnum.CcBySa30,
            ["cc by sa 4.0"] = CopyrightLicenseEnum.CcBySa40,

            ["cc by 1.0"] = CopyrightLicenseEnum.CcBy10,
            ["cc by 2.0"] = CopyrightLicenseEnum.CcBy20,
            ["cc by 2.5"] = CopyrightLicenseEnum.CcBy25,
            ["cc by 3.0"] = CopyrightLicenseEnum.CcBy30,
            ["cc by 4.0"] = CopyrightLicenseEnum.CcBy40,

            ["cc0"] = CopyrightLicenseEnum.CcZero10,
            ["pdm owner"] = CopyrightLicenseEnum.CcPdm10,
            ["public domain"]


            ["gfdl 1.2"] = CopyrightLicenseEnum.Gfdl12,
            ["gfdl 1.3"] = CopyrightLicenseEnum.Gfdl13,
            ["gfdl 1.3 only"] = CopyrightLicenseEnum.Gfdl13,

        };
        private IMediaWikiConstantsContainer _constants;

        public Tokenizer(IMediaWikiConstantsContainer mediaWikiConstantsContainer)
        {
            _constants = mediaWikiConstantsContainer;
        }

        public CopyrightLicenseEnum GetCopyrightLicense(IEnumerable<KeyValuePair<string, string>> attributes)
        {
            foreach (var attribute in attributes)
            {
                var normalizedKey = attribute.Key.ToLower();

                var isLicensingAttribute = 
                    normalizedKey == "license" || 
                    normalizedKey == "licenseshortname";

                if (!isLicensingAttribute)
                {
                    continue;
                }

                var normalizedValue = attribute.Value.ToLower().Replace("_", " ").Replace("-", " ");

                if (_publicDomainEquivalentNames.Contains(normalizedValue))
                {
                    return CopyrightLicenseEnum.PublicDomainEquivalent;
                }

                // disregard anything that comes after CC BY-SA X.Y, like translations or dual licensing notes
                if (Regex.IsMatch(normalizedValue, "cc by sa [0-9]\\.[0-9]"))
                {
                    return CopyrightLicenseEnum.CcBySa;
                }

                // disregard anything that comes after CC BY X.Y, like translations or dual licensing notes
                else if (Regex.IsMatch(normalizedValue, "cc by [0-9]\\.[0-9]"))
                {
                    return CopyrightLicenseEnum.CcBy;
                }

                // disregard anything that comes after GDFL 1.X, like translations or dual licensing notes
                else if (Regex.IsMatch(normalizedValue, "gdfl [0-9]\\.[0-9]"))
                {
                    return CopyrightLicenseEnum.Gfdl;
                }
            }
            return CopyrightLicenseEnum.Unknown;
        }

        public LicenseRestrictionsEnum GetLicenseRestrictions(IEnumerable<KeyValuePair<string, string>> attributes)
        {
            throw new NotImplementedException();
        }
    }
}
