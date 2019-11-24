using Cinema.Persisted.Entities;
using Cinema.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Web.Providers.Interfaces
{
    public interface IFilmSearcherProvider
    {
        Task<List<Film>> GetBySearchModelAsync(FilmSearchModel model);
        Task<List<Film>> GetAllAsync();
    }
}
