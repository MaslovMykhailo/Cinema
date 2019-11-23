using Cinema.Persisted.Entities;
using Cinema.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Web.Providers.Interfaces
{
    public interface IFilmExplorerProvider
    {
        Task<List<Film>> GetBySearchModelAsync(FilmSearchModel model);

        Task<List<Guid>> GetFilmIdsAsync();

        Task<List<Film>> GetFilmByIdsAsync(List<Guid> ids);

        Task<List<Film>> GetAllAsync();
    }
}
