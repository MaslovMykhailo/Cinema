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
        private static TimeSpan CACHE_UPDATE_TIMEOUT = TimeSpan.FromMinutes(20);

        private readonly IFilmSearcherProvider _searchProvider;
        private readonly IFilmExplorerProvider _explorerProvider;
        private readonly IMemoryCache _cache;

        public FilmProvider(IFilmSearcherProvider searchProvider, IFilmExplorerProvider explorerProvider, IMemoryCache cache)
        {
            _searchProvider = searchProvider;
            _explorerProvider = explorerProvider;
            _cache = cache;

            _ = UpdateCacheByTimeout();
        }

        public async Task<List<Film>> GetBySearchModelAsync(FilmSearchModel model)
        {
            var searcherResult = await _searchProvider.GetBySearchModelAsync(model);
            var explorerResult = await _explorerProvider.GetBySearchModelAsync(model);

            return searcherResult.Concat(explorerResult).ToList();
        }

        private async Task<List<Film>> GetAllAsync()
        {
            var results = await Task.WhenAll(_searchProvider.GetAllAsync(), _explorerProvider.GetAllAsync());
            return results[0].Concat(results[1]).ToList();
        }

        private async Task<List<Film>> GetAllFromCacheAsync()
        {
            return await _cache.GetOrCreateAsync("_Films", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = CACHE_UPDATE_TIMEOUT;
                return await GetAllAsync();
            });
        }

        private async Task UpdateCacheByTimeout()
        {
            await GetAllFromCacheAsync();
            await Task.Delay(CACHE_UPDATE_TIMEOUT).ConfigureAwait(false);
            _ = UpdateCacheByTimeout();
        }

        public async Task<List<Film>> GetBySearchModelCachedAsync(PagedFilmSearchModel model)
        {
            var films = await GetAllFromCacheAsync();
            var filter = new FilmFilterBuilder(FilmSearchModel.Ensure(model)).Build().Compile();

            return films
                .Where(filter)
                .OrderBy(film => film.Name)
                .Skip(model.Page > 1 ? (model.Page - 1) * PagedFilmSearchModel.PAGE_SIZE : 0)
                .Take(PagedFilmSearchModel.PAGE_SIZE)
                .ToList();
        }
    }
}
