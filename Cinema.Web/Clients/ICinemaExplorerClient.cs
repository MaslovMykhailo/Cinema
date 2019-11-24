using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Web.Clients
{
    public interface ICinemaExplorerClient
    {
        [Get("/api/film/{id}")]
        Task<T> GetAsync<T>(string id);

        [Get("/api/film/price-list")]
        Task<IEnumerable<T>> GetPriceList<T>();

        [Get("/api/film/film-ids")]
        Task<List<T>> GetByIdsAsync<T>([Body] List<Guid> ids);

        [Get("/api/film")]
        Task<List<T>> GetAllAsync<T>();
    }
}
