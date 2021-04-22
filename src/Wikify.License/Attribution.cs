using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wikify.Common.License;

namespace Wikify.License
{
    public class Attribution : IAttribution
    {
        public string Title { get; }
        public string Author { get; }
        public string Credit { get; }

        public Attribution(string title, string author, string credit)
        {
            Title = title;
            Author = author;
            Credit = credit;
        }

    }
}
