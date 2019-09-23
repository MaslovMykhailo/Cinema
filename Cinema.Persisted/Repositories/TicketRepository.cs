using Cinema.Persisted.Context;
using Cinema.Persisted.Entities;
using Cinema.Persisted.Interfaces;

namespace Cinema.Persisted.Repositories
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(CinemaContext context) : base(context)
        {

        }
    }
}
