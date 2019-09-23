using Cinema.Persisted.Context;
using Cinema.Persisted.Entities;
using Cinema.Persisted.Interfaces;

namespace Cinema.Persisted.Repositories
{
    public class PlaceRepository : GenericRepository<Place>, IPlaceRepository
    {
        public PlaceRepository(CinemaContext context) : base(context)
        {

        }
    }
}
