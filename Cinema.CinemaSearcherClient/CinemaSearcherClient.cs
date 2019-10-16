using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.CinemaSearcherClient
{
    public class CinemaSearcherClient
    {
        private readonly IHttpClientFactory _clientFactory;

        public IEnumerable<object> Tickets { get; private set; }

        public bool GetBranchesError { get; private set; }

        public CinemaSearcherClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task OnGet()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://localhost:44377/api/ticket");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Tickets = await response.Content
                    .ReadAsAsync<IEnumerable<object>>();
            }
            else
            {
                GetBranchesError = true;
                Tickets = Array.Empty<object>();
            }
        }
    }
}
