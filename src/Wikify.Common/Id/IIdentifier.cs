using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content;

namespace Wikify.Common.Id
{
    public interface IIdProvider
    {
        public Task<IIdentifier> GetIdentifierAsync(IIdUserInput idUserInput);
    }

    public interface IIdentifier
    {
        public Uri Uri { get; }
        public Uri LicenseUri { get; }
    }

    //public interface IIdentifier<T> : IIdentifier where T : IWikiMedia
    //{
    //    // TODO : not sure if I'm gonna need this
    //}
}
