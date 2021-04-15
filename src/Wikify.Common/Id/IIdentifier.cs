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
        public string Title { get; }

    }

    public interface IArticleIdentifier : IIdentifier
    {
        public Uri GetEndpoint(WikiContentModel contentModel);
    }

    public interface IImageIdentifier : IIdentifier
    {
        public Uri Endpoint { get; }
    }

}
