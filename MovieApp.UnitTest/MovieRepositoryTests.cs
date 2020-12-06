using Moq;
using MovieApp.DataSubSystem.Contract;
using MovieApp.Domain.Models;
using MovieApp.Persistence;
using MovieApp.Persistence.Contract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MovieApp.UnitTest
{
    public class MovieRepositoryTests
    {
        private IMovieRepository movieRepository;

        public MovieRepositoryTests()
        {
            //Setup JSONRW   
            var jsonRWMock = new Mock<IJsonRW>();
            jsonRWMock.Setup(repo => repo.Read<Movie>()).ReturnsAsync(TestMovies());

            movieRepository = new MovieRepository(jsonRWMock.Object);
        }

        [Fact]
        [Trait("MovieRepositoryTest", "Unit")]
        public async Task GetMovies_ReturnsAllMoviesAsQueryable_FromFile()
        {
            //arrange & act
            var movies = await movieRepository.GetMovies();

            //assert
            Assert.NotNull(movies);
            Assert.IsAssignableFrom<IQueryable<Movie>>(movies);
        }

        private IEnumerable<Movie> TestMovies()
        {
            var movies = new List<Movie>
            {
                new Movie { title = "Movie 1" },
                new Movie { title = "Movie 2" }
            };

            return movies;
        }
    }
}
