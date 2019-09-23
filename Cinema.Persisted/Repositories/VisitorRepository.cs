using Cinema.Persisted.Context;
using Cinema.Persisted.Entities;
using Cinema.Persisted.Interfaces;

namespace Cinema.Persisted.Repositories
{
    public class VisitorRepository : GenericRepository<Visitor>, IVisitorRepository
    {
        public VisitorRepository(CinemaContext context) : base(context)
        {

        }
    }
}
