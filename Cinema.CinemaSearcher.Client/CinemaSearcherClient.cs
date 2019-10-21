using Cinema.Persisted.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cinema.CinemaSearcher.Client
{
    public class CinemaSearcherClient : ICinemaSearcherClient
    {
        public HttpClient Client { get; set; }

        public CinemaSearcherClient()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44377/");
            Client.DefaultRequestHeaders.Add("Accept", "application/json");
            Client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
        }

        public async Task<IEnumerable<T>> GetAsync<T>()
        {
            var response = await Client.GetAsync("api/film");

            response.EnsureSuccessStatusCode();

            var result = await response.Content
                .ReadAsAsync<IEnumerable<T>>();

            return result;
        }

        public async Task<IEnumerable<T>> GetBySearchQueryAsync<T>(QueryString queryString)
        {
            UriBuilder builder = new UriBuilder(String.Format("https://localhost:44377/api/film/search"));
            builder.Query = queryString.ToString();
            var response = await Client.GetAsync(builder.Uri);

            response.EnsureSuccessStatusCode();

            var result = await response.Content
                .ReadAsAsync<IEnumerable<T>>();

            return result;
        }
    }
}
