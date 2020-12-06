using MovieApp.DataSubSystem.Contract;
using MovieApp.Domain.Models;
using MovieApp.Persistence.Contract;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Persistence
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IJsonRW jsonRw;

        public MovieRepository(IJsonRW jsonRw)
        {
            this.jsonRw = jsonRw;
        }

        public async Task<IQueryable<Movie>> GetMovies()
        {
            var allMovies = await jsonRw.Read<Movie>();

            return allMovies.AsQueryable();
        }
    }
}
