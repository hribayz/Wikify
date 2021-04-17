using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wikify.Common.Content;

namespace Wikify.Common.Id
{

    public interface IIdentifier
    {
        public string Title { get; }

    }

    public interface IArticleIdentifier : IIdentifier
    {
        public LanguageEnum Language { get; }
    }

    public interface IImageIdentifier : IIdentifier
    {

    }

}
