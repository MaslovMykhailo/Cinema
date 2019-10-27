using System.Threading.Tasks;

namespace Cinema.Persisted.Interfaces
{
        public interface IUnitOfWork
        {
            IFilmRepository FilmRepository { get; }

            IHallRepository HallRepository { get; }

            IPlaceRepository PlaceRepository { get; }

            ITicketRepository TicketRepository { get; }

            IVisitorRepository VisitorRepository { get; }

            ISessionRepository SessionRepository { get; }

            Task CommitAsync();
        }
}
