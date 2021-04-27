using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wikify.Common.MediaWikiModels
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class ImageInfoResponse
    {
        //public string? batchcomplete { get; set; }
        public Query? query { get; set; }
    }

    public class Query
    {
        public Normalized[]? normalized { get; set; }
        public Dictionary<int, Page?>? pages { get; set; }
    }

    public class Page
    {
        //public int? ns { get; set; }
        //public string? title { get; set; }
        //public string? missing { get; set; }
        //public string? known { get; set; }
        //public string? imagerepository { get; set; }

        public Imageinfo[] imageinfo { get; set; }
    }

    public class Imageinfo
    {
        public Dictionary<string, Metadata> extmetadata { get; set; }
    }


    public class Metadata
    {
        public string value { get; set; }
        //public string source { get; set; }
        //public string hidden { get; set; }
    }


    public class Normalized
    {
        public string from { get; set; }
        public string to { get; set; }
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
