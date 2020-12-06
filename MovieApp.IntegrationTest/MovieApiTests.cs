using MovieApp.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MovieApp.IntegrationTest
{
    public class MovieApiTests
    {
        [Fact]
        [Trait("MovieApiTests", "API")]
        public async Task GetLatestMovies_ReturnsLatestFourMovies()
        {
            var context = new TestContext();

            using (var client = context.Client)
            {
                var response = await client.GetAsync("/api/movie/latest/4");

                Assert.True(response.IsSuccessStatusCode);

                var movies = await response.Content.ReadAsJsonAsync<IEnumerable<Movie>>();

                Assert.NotNull(movies);
                Assert.IsAssignableFrom<IEnumerable<Movie>>(movies);
                Assert.Equal(2018, movies.First().year);
                Assert.Equal("Halloween III", movies.First().title);

            }
        }

        [Fact]
        [Trait("MovieApiTests", "API")]
        public async Task GetLatestMovies_ThrowsBadRequest_WithNonNumericCount()
        {
            var context = new TestContext();

            using var client = context.Client;
            var response = await client.GetAsync("/api/movie/latest/xyz");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        [Trait("MovieApiTests", "API")]

        public async Task GetLatestMovies_ThrowsNotFound_WithCountZero()
        {
            var context = new TestContext();

            using var client = context.Client;
            var response = await client.GetAsync("/api/movie/latest/0");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        [Trait("MovieApiTests", "API")]
        public async Task GetMovieDetails_ReturnsMovieDetails_DirectorAndTitle()
        {
            var context = new TestContext();

            using (var client = context.Client)
            {
                var title = "The Clinic";
                var director = "James Rabbitts";

                var response = await client.GetAsync(string.Format("/api/movie/details?title={0}&director={1}",title, director));

                Assert.True(response.IsSuccessStatusCode);

                var movie = await response.Content.ReadAsJsonAsync<Movie>();

                Assert.NotNull(movie);
                Assert.IsAssignableFrom<Movie>(movie);
                Assert.Equal(2010, movie.year);

            }
        }

        [Fact]
        [Trait("MovieApiTests", "API")]

        public async Task GetMovieDetails_ThrowsNotFound_WithNoDirector()
        {
            var context = new TestContext();

            using var client = context.Client;
            var response = await client.GetAsync(string.Format("/api/movie/details?title={0}&director={1}", "Titanic", null));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        [Trait("MovieApiTests", "API")]
        public async Task SearchMovies_ReturnsListOfMovies_MatchesTheDirector()
        {
            var context = new TestContext();

            using (var client = context.Client)
            {
                var director = "Sam Mendes";

                var response = await client.GetAsync(string.Format("/api/movie/search?director={0}", director));

                Assert.True(response.IsSuccessStatusCode);

                var movies = await response.Content.ReadAsJsonAsync<IEnumerable<Movie>>();

                //assert
                Assert.NotNull(movies);
                Assert.IsAssignableFrom<ICollection<Movie>>(movies);
                Assert.True(movies.All(m => m.info.directors.Contains("Sam Mendes")));

            }
        }

        [Fact]
        [Trait("MovieApiTests", "API")]

        public async Task SearchMovies_ThrowsNotFound_WithNoDirector()
        {
            var context = new TestContext();

            using var client = context.Client;
            var response = await client.GetAsync(string.Format("/api/movie/search?director={0}", "Nilanjan"));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
