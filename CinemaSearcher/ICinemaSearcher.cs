using Microsoft.AspNetCore.Http;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaSearcher
{
    public interface ICinemaSearcher
    {
        [Get("/api/film")]
        Task<IEnumerable<T>> GetAsync<T>();

        [Get("/api/search")]
        Task<IEnumerable<T>> GetBySearchQueryAsync<T>([Query]FilmSearchModel);
    }
}
