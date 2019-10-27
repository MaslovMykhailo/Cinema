using Cinema.Web.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaSearcher
{
    public interface ICinemaSearcherClient
    {
        [Get("/api/film")]
        Task<IEnumerable<T>> GetAsync<T>();

        [Get("/api/film/search")]
        Task<IEnumerable<T>> GetBySearchQueryAsync<T>(FilmSearchModel query);
    }
}
