using MovieApp.Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Persistence.Contract
{
    public interface IMovieRepository
    {
        /// <summary>
        /// Gets all movies from the json
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<Movie>> GetMovies();

    }
}
