using System;

namespace MovieApp.Domain.Models
{
    public class MovieSearchCriteria
    {
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Title { get; set; }

        public string Actor { get; set; }

        public string Director { get; set; }

        public DateTime ReleaseDate { get; set; }

    }
}
