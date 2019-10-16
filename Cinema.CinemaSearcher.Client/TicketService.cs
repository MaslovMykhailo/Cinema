using Cinema.Persisted.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cinema.CinemaSearcher.Client
{
    public class TicketService : ITicketService
    {
        public HttpClient Client { get; }

        public TicketService()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44377/");
            Client.DefaultRequestHeaders.Add("Accept", "application/json");
            Client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
        }

        public async Task<IEnumerable<Ticket>> GetAllTickets()
        {
            var response = await Client.GetAsync("api/ticket");

            response.EnsureSuccessStatusCode();

            var result = await response.Content
                .ReadAsAsync<IEnumerable<Ticket>>();

            return result;
        }

        public async Task<IEnumerable<Ticket>> GetBySearchQuery(QueryString queryString)
        {
            UriBuilder builder = new UriBuilder("https://localhost:44377/api/ticket/search");
            builder.Query = queryString.ToString();
            var response = await Client.GetAsync(builder.Uri);

            response.EnsureSuccessStatusCode();

            var result = await response.Content
                .ReadAsAsync<IEnumerable<Ticket>>();

            return result;
        }
    }
}
