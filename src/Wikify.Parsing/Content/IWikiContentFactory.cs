using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content;

namespace Wikify.Parsing.Content
{
    public interface IWikiContentFactory
    {
        public IWikiComponent CreateComponent(WikiComponentType componentType, IReadOnlyCollection<IWikiComponent> children);
    }
}
