using System;
using System.Collections.Generic;

namespace MovieFnApp.Entities
{
    public partial class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public int ReleaseYear { get; set; }
        public string Actor { get; set; }
        public string Director { get; set; }
        public string BannerUrl { get; set; }
    }
}
