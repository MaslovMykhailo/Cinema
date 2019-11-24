using Cinema.Web.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Web.Clients
{
    public interface ICinemaSearcherClient
    {
        [Get("/api/film")]
        Task<List<T>> GetAllAsync<T>();

        [Get("/api/film/search")]
        Task<IEnumerable<T>> GetBySearchQueryAsync<T>(FilmSearchModel query);
    }
}
