using Refit;
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
    }
}
