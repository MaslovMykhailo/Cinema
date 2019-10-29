using Cinema.Persisted.Entities;
using Cinema.Web.Clients;
using Cinema.Web.Models;
using Cinema.Web.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Web.Providers.FilmProviders
{
    public class FilmProvider : IFilmProvider
    {
        private readonly FilmSearcherProvider _searchProvider; 
        private readonly FilmExplorerProvider _explorProvider;
        public FilmProvider(ICinemaSearcherClient searcherClient, ICinemaExplorerClient explorerClient)
        {
            _searchProvider = new FilmSearcherProvider(searcherClient);
            _explorProvider = new FilmExplorerProvider(explorerClient);
        }

        public async Task<List<Film>> GetBySearchModelAsync(FilmSearchModel model)
        {
            var searcherResult = await _searchProvider.GetBySearchModelAsync(model);
            var explorerResult = await _explorProvider.GetBySearchModelAsync(model);

            return searcherResult.Concat(explorerResult).ToList();
        }
    }
}
