using System;

namespace MovieApp.Domain.Models
{
    public class Movie
    {
        public int year { get; set; }
        public string title { get; set; }
        public MovieInfo info { get; set; }
    }

    public class MovieInfo
    {
        public string[] directors { get; set; }
        public DateTime release_date { get; set; }
        public float rating { get; set; }
        public string[] genres { get; set; }
        public string image_url { get; set; }
        public string plot { get; set; }
        public int rank { get; set; }
        public int running_time_secs { get; set; }
        public string[] actors { get; set; }
    }
}

