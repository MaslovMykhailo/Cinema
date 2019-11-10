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
    public class FilmExplorerProvider : IFilmProvider 
    {
        private readonly ICinemaExplorerClient _explorerClient;

        public FilmExplorerProvider(ICinemaExplorerClient client)
        {
            _explorerClient = client;
        }

        private async Task<List<Guid>> GetFilmIdsAsync()
        {
            var priceList = (await _explorerClient.GetPriceList<FilmPrice>()).ToList();
            return priceList.ConvertAll(_ => _.FilmId);
        }

        private Task<Film> GetFilmByIdAsync(Guid id)
        {
            return _explorerClient.GetAsync<Film>(id.ToString());
        }

        public async Task<List<Film>> GetBySearchModelAsync(FilmSearchModel model)
        {
            var filmIds = await GetFilmIdsAsync();
            var films = await Task.WhenAll(filmIds.ConvertAll(id => GetFilmByIdAsync(id)));
            var filter = new FilmFilterBuilder(FilmSearchModel.Ensure(model)).Build().Compile();

            return films.Where(filter).ToList();
        }
    }
}
