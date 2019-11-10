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
    public class FilmSearcherProvider : IFilmProvider
    {
        private readonly ICinemaSearcherClient _searcherClient;
        public FilmSearcherProvider(ICinemaSearcherClient client)
        {
            _searcherClient = client;
        }

        public async Task<List<Film>> GetBySearchModelAsync(FilmSearchModel model)
        {
            return (await _searcherClient.GetBySearchQueryAsync<Film>(model)).ToList();
        }
    }
}
