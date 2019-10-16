using Cinema.BusinessLogic.Searching;
using Cinema.Persisted.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Cinema.CinemaSearcher.Client
{
    public class CinemaSearcherClient : ICinemaSearcherClient
    {
        private readonly ITicketService _ticketService;

        public IEnumerable<Ticket> Tickets { get; private set; }

        public bool GetIssuesError { get; private set; }

        public CinemaSearcherClient(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<IEnumerable<Ticket>> GetBySearchQuery(QueryString queryString)
        {
            try
            {
                Tickets = await _ticketService.GetBySearchQuery(queryString);
            }
            catch (HttpRequestException)
            {
                GetIssuesError = true;
                Tickets = Array.Empty<Ticket>();
            }

            return Tickets;
        }
    }
}
