using MovieApp.DataSubSystem;
using MovieApp.Domain.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace MovieApp.UnitTest
{
    [ExcludeFromCodeCoverage]
    public class JsonRWTests
    {
 
        [Fact]
        [Trait("JsonRWTests", "Unit")]
        public async Task Read_ReturnsMovies_FromFile()
        {
            //arrange
            var jsonRW = new JsonRW("./Resource/moviedata.json");

            //act
            var movieRaw = await jsonRW.Read<Movie>();

            //assert
            Assert.NotNull(movieRaw);
            Assert.IsAssignableFrom<IEnumerable<Movie>>(movieRaw);
        }

        [Fact]
        [Trait("JsonRWTests", "Unit")]

        public async Task Read_ThrowsFileNotFound_FileNotPresent()
        {
            //arrange
            var jsonRW = new JsonRW("./Resource/moviedata1.json");
            // act & assert
            await Assert.ThrowsAsync<FileNotFoundException>(() => jsonRW.Read<Movie>());
        }

        [Fact]
        [Trait("JsonRWTests", "Unit")]

        public async Task Read_ThrowsJsonException_MovieJsonIsNotWellformed()
        {
            //arrange
            var tempJsonRW = new JsonRW("./Resource/moviedata_dirty.json");
            // act & assert
            await Assert.ThrowsAsync<JsonException>(() => tempJsonRW.Read<Movie>());
        }
    }
}
