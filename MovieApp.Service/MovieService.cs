using MovieApp.Domain.Models;
using MovieApp.Persistence.Contract;
using MovieApp.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MovieApp.Service
{
    // </inheritdoc>
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        // </inheritdoc>
        public async Task<ICollection<Movie>> GetLatestMovies(int count)
        {
            var movies = await movieRepository.GetMovies();

            return movies.OrderByDescending(movie => movie.year).Take(count).ToList();

        }

        // </inheritdoc>
        public async  Task<Movie> GetMovieDetails(string title, string director)
        {
            var movies = await movieRepository.GetMovies();
            return movies.SingleOrDefault(movie => movie.title == title 
                                                    && movie.info.directors.Contains(director));
        }

        // </inheritdoc>
        public async Task<ICollection<Movie>> SearchMovies(MovieSearchCriteria searchCriteria)
        {
            var movies = await movieRepository.GetMovies();
            return movies.Where(movie => (!string.IsNullOrEmpty(searchCriteria.Title) && movie.title.Contains(searchCriteria.Title, StringComparison.InvariantCultureIgnoreCase)
                           || (searchCriteria.Year != null && movie.year == searchCriteria.Year)
                           || (!string.IsNullOrEmpty(searchCriteria.Director) && movie.info.directors != null && movie.info.directors.Contains(searchCriteria.Director))
                           || (!string.IsNullOrEmpty(searchCriteria.Actor) && movie.info.actors != null && movie.info.actors.Contains(searchCriteria.Actor))
                           || (searchCriteria.ReleaseDate != DateTime.MinValue && movie.info.release_date != null && movie.info.release_date == searchCriteria.ReleaseDate)
                           || (!string.IsNullOrEmpty(searchCriteria.Genre) && movie.info.genres != null && movie.info.genres.Contains(searchCriteria.Genre))
                           )).ToList();
        }
    }
}
