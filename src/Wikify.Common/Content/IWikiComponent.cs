using System.Collections.Generic;
using Wikify.Common.License;

namespace Wikify.Common.Content
{
    public interface IWikiComponent
    {
        public IEnumerable<IWikiComponent> Children { get; }
        public WikiComponentType ComponentType { get; }
    }
}
