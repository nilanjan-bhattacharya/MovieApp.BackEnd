using MovieApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Contracts
{
    public interface IMovieService
    {
        /// <summary>Returns latest movies by year.</summary>
        /// <param name="count">Number of latest movies to be fetched.</param>
        Task<ICollection<Movie>> GetLatestMovies(int count);

        /// <summary>Returns details of movie by name and director.</summary>
        /// <param name="title">Title of the movie.</param>
        /// <param name="director">Director of movie.</param>
        Task<Movie> GetMovieDetails(string title, string director);

        /// <summary>Search movies by criteria.</summary>
        /// <param name="searchCriteria"><see cref="MovieSearchCriteria"/> </param>
        Task<ICollection<Movie>> SearchMovies(MovieSearchCriteria searchCriteria);

    }
}
