using Cinema.Persisted.Context;
using Cinema.Persisted.Entities;
using Cinema.Persisted.Interfaces;

namespace Cinema.Persisted.Repositories
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        public SessionRepository(CinemaContext context) : base(context)
        {
        }
    }
}
