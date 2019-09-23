using Cinema.Persisted.Context;
using Cinema.Persisted.Entities;
using Cinema.Persisted.Interfaces;

namespace Cinema.Persisted.Repositories
{
    public class FilmRepository : GenericRepository<Film>, IFilmRepository
    {
        public FilmRepository(CinemaContext context) : base(context)
        {
        }        
    }
}
