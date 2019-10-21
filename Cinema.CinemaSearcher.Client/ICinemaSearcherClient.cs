using Cinema.Persisted.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.CinemaSearcher.Client
{
    public interface ICinemaSearcherClient
    {
        Task<IEnumerable<T>> GetAsync<T>();
        Task<IEnumerable<T>> GetBySearchQueryAsync<T>(QueryString queryString);
    }
}
