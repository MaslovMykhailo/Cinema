using Cinema.Persisted.Entities;
using Cinema.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Web.Providers.Interfaces
{
    public interface IFilmProvider
    {
        Task<List<Film>> GetBySearchModelAsync(FilmSearchModel model);
    }
}
