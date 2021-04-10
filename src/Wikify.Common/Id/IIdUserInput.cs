using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.Content;

namespace Wikify.Common.Id
{
    public interface IIdUserInput
    {

    }
    public interface IIdUserInput<T> : IIdUserInput where T : IWikiMedia
    {

    }
}
