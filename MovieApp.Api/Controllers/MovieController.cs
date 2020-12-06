using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Domain.Models;
using MovieApp.Service.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MovieApp.Api.Controllers
{
    [Route("api/movie")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService movieService;

        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        /// <summary>
        /// Returns latest movies by year.
        /// </summary>
        /// <param name="count">Number of movies to be fetched</param>
        /// <response code="200">The movies successfully fetched.</response>
        /// <response code="404">The movies not found.</response>
        /// <response code="500">An error occurred when getting the movies.</response>
        [HttpGet("latest/{count}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ICollection<Movie>>> GetLatestMovies(int count)
        {
            var movies =  await movieService.GetLatestMovies(count);

            if(movies.Count == 0)
            {
                return NotFound();
            }

            return Ok(movies);
        }

        /// <summary>Returns details of movie by name and director.</summary>
        /// <param name="name">Name of movie.</param>
        /// <param name="director">Director of movie.</param>
        /// <response code="200">The movie successfully fetched.</response>
        /// <response code="404">The movie not found.</response>
        /// <response code="500">An error occurred when getting the movie.</response>
        [HttpGet("details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Movie>> GetMovieDetails(string name, string director)
        {
            var movie = await movieService.GetMovieDetails(name, director);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        /// <summary>Search movies by criteria.</summary>
        /// <param name="searchCriteria"><see cref="MovieSearchCriteria"/> </param>
        /// <response code="200">The movies successfully fetched.</response>
        /// <response code="404">The movies not found.</response>
        /// <response code="500">An error occurred when getting the movies.</response>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ICollection<Movie>>> SearchMovies([FromQuery] MovieSearchCriteria searchCriteria)
        {
            var movies = await movieService.SearchMovies(searchCriteria);

            if (movies.Count == 0)
            {
                return NotFound();
            }

            return Ok(movies);
        }


    }
}
