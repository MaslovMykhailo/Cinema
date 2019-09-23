using System;

namespace Cinema.Persisted.Entities
{
    public class Session : BaseEntity
    {
        public Guid? FilmId { get; set; }

        public virtual Film Film { get; set; }

        public DateTime Time { get; set; }

        public Guid? HallId { get; set; }

        public virtual Hall Hall { get; set; }

        public int TicketsSaled { get; set; }
    }
}
