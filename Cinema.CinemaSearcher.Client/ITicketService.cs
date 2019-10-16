using Cinema.BusinessLogic.Searching;
using Cinema.Persisted.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.CinemaSearcher.Client
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetAllTickets();
        Task<IEnumerable<Ticket>> GetBySearchQuery(QueryString queryString);
    }
}
