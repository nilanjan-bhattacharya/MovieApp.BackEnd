using Moq;
using MovieApp.Domain.Models;
using MovieApp.Persistence.Contract;
using MovieApp.Service;
using MovieApp.Service.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace MovieApp.UnitTest
{
    public class MovieServiceTests
    {
        private readonly IMovieService movieService;

        public MovieServiceTests()
        {
            //Setup Movie repository  
            var movieRepoMock = new Mock<IMovieRepository>();
            movieRepoMock.Setup(repo => repo.GetMovies()).ReturnsAsync(TestMovies());

            movieService = new MovieService(movieRepoMock.Object);
        }

        [Fact]
        [Trait("MovieServiceTests", "Unit")]
        public async Task GetLatestMovies_ReturnsLatestMovies_PerCount()
        {
            //arrange & act
            var movies = await movieService.GetLatestMovies(4);

            //assert
            Assert.NotNull(movies);
            Assert.IsAssignableFrom<IEnumerable<Movie>>(movies);
            Assert.Equal(2018, movies.First().year);
            Assert.Equal("Halloween III", movies.First().title);
            
        }

        [Fact]
        [Trait("MovieServiceTests", "Unit")]
        public async Task GetMovieDetails_ReturnsSingleMovie_AccordingToDirectorAndTitle()
        {
            //arrange & act
            var movie = await movieService.GetMovieDetails("Repo Man", "Alex Cox");

            //assert
            Assert.NotNull(movie);
            Assert.IsAssignableFrom<Movie>(movie);
            Assert.Equal(1984, movie.year);
            Assert.Equal(6.7, Math.Round(movie.info.rating, 1));

        }

        [Fact]
        [Trait("MovieServiceTests", "Unit")]
        public async Task SearchMovies_ReturnsMovies_ReleasedIn2018()
        {
            //arrange & act
            var movies = await movieService.SearchMovies(new MovieSearchCriteria { Year = 2018 });

            //assert
            Assert.NotNull(movies);
            Assert.IsAssignableFrom<ICollection<Movie>>(movies);
            Assert.True(movies.All(m => m.year == 2018));

        }

        [Fact]
        [Trait("MovieServiceTests", "Unit")]
        public async Task SearchMovies_ReturnsMovies_DirectedByPatrickLussier()
        {
            //arrange & act
            var movies = await movieService.SearchMovies(new MovieSearchCriteria { Director = "Patrick Lussier" });

            //assert
            Assert.NotNull(movies);
            Assert.IsAssignableFrom<ICollection<Movie>>(movies);
            Assert.True(movies.All(m => m.info.directors.Contains("Patrick Lussier")));

        }

        private IQueryable<Movie> TestMovies()
        {
            //{
            //  "year": 2018,
            //  "title": "Halloween III",
            //  "info": {
            //            "directors": ["Patrick Lussier"],
            //    "genres": ["Horror"],
            //    "plot": "The plot is unknown at this time.",
            //    "rank": 4896
            //  }
            //}
            var jsonString = File.ReadAllText("./Resource/moviedata.json");

            return JsonSerializer.Deserialize<IEnumerable<Movie>>(jsonString).AsQueryable();
        }
    }
}
