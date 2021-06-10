using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content.Parsed;

namespace Wikify.Builder.Specifications
{
    public abstract class BuildSpecification<T> where T : IWikiComponent
    {
        public abstract Func<T, T> Expression { get; }
    }

}
