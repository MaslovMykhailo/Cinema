using Cinema.Persisted.Context;
using Cinema.Persisted.Entities;
using Cinema.Persisted.Interfaces;

namespace Cinema.Persisted.Repositories
{
    public class HallRepository : GenericRepository<Hall>, IHallRepository
    {
        public HallRepository(CinemaContext context) : base(context)
        {

        }
    }
}
