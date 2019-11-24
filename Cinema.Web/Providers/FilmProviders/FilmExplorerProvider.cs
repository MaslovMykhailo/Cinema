using Cinema.Persisted.Entities;
using Cinema.Web.Clients;
using Cinema.Web.Models;
using Cinema.Web.Providers.Interfaces;
using Cinema.Web.Providers.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Web.Providers.FilmProviders
{
    public class FilmExplorerProvider : IFilmExplorerProvider 
    {
        private readonly ICinemaExplorerClient _explorerClient;

        public FilmExplorerProvider(ICinemaExplorerClient client)
        {
            _explorerClient = client;
        }

        public async Task<List<Guid>> GetFilmIdsAsync()
        {
            var priceList = (await _explorerClient.GetPriceList<FilmPrice>()).ToList();
            return priceList.ConvertAll(_ => _.FilmId);
        }

        public async Task<List<Film>> GetFilmByIdsAsync(List<Guid> ids)
        {
            return await _explorerClient.GetByIdsAsync<Film>(ids);
        }

        public async Task<List<Film>> GetBySearchModelAsync(FilmSearchModel model)
        {
            var filmIds = await GetFilmIdsAsync();
            var films = await GetFilmByIdsAsync(filmIds);
            var filter = new FilmFilterBuilder(FilmSearchModel.Ensure(model)).Build().Compile();

            return films.Where(filter).ToList();
        }

        public async Task<List<Film>> GetAllAsync()
        {
            return await _explorerClient.GetAllAsync<Film>();
        }
    }
}
