using Cinema.Persisted.Entities;
using Cinema.Web.Models;
using Cinema.Web.Providers.Interfaces;
using Cinema.Web.Providers.Utils;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Web.Providers.FilmProviders
{
    public class FilmProvider : IFilmProvider
    {
        private readonly IFilmSearcherProvider _searchProvider;
        private readonly IFilmExplorerProvider _explorerProvider;
        private readonly IMemoryCache _cache;

        //TODO: Refresh cache with background worker or smth else
        private readonly BackgroundWorker _backgroundWorker;

        public FilmProvider(IFilmSearcherProvider searchProvider, IFilmExplorerProvider explorerProvider, IMemoryCache cache)
        {
            _searchProvider = searchProvider;
            _explorerProvider = explorerProvider;
            _cache = cache;
            _backgroundWorker = new BackgroundWorker();
        }

        public async Task<List<Film>> GetBySearchModelAsync(FilmSearchModel model)
        {
            var searcherResult = await _searchProvider.GetBySearchModelAsync(model);
            var explorerResult = await _explorerProvider.GetBySearchModelAsync(model);

            return searcherResult.Concat(explorerResult).ToList();
        }

        public async Task<List<Film>> GetBySearchModelCachedAsync(FilmSearchModel model)
        {
            var films = await _cache.GetOrCreateAsync("_Films", async entry =>
            {
                entry.SetSlidingExpiration(TimeSpan.FromSeconds(90));
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(120);
                var searcherResult = await _searchProvider.GetAllAsync();
                var explorerResult = await _explorerProvider.GetAllAsync();

                return searcherResult.Concat(explorerResult).ToList();
            });

            var filter = new FilmFilterBuilder(FilmSearchModel.Ensure(model)).Build().Compile();

            return films.Where(filter).ToList();
        }
    }
}
