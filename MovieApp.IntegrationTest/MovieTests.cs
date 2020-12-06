using MovieApp.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MovieApp.IntegrationTest
{
    public class MovieTests
    {
        [Fact]
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
        public async Task GetLatestMovies_Throws()
        {
            var context = new TestContext();

            using var client = context.Client;
            var response = await client.GetAsync("/api/movie/latest/xyz");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
