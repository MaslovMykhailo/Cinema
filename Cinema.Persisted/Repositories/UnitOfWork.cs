using Cinema.Persisted.Context;
using Cinema.Persisted.Interfaces;
using System.Threading.Tasks;

namespace Cinema.Persisted.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CinemaContext _context;

        private IFilmRepository _filmRepository;
        private IHallRepository _hallRepository;
        private IPlaceRepository _placeRepository;
        private ITicketRepository _ticketRepository;
        private IVisitorRepository _visitorRepository;

        public UnitOfWork(CinemaContext context)
        {
            _context = context;
        }

        public IFilmRepository FilmRepository => _filmRepository ?? (_filmRepository = new FilmRepository(_context));

        public IHallRepository HallRepository => _hallRepository ?? (_hallRepository = new HallRepository(_context));

        public IPlaceRepository PlaceRepository => _placeRepository ?? (_placeRepository = new PlaceRepository(_context));

        public ITicketRepository TicketRepository => _ticketRepository ?? (_ticketRepository = new TicketRepository(_context));

        public IVisitorRepository VisitorRepository => _visitorRepository ?? (_visitorRepository = new VisitorRepository(_context));

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
