using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Web.Clients
{
    public interface ICinemaExplorerClient
    {
        [Get("/api/film")]
        Task<IEnumerable<T>> GetAsync<T>();
    }
}
