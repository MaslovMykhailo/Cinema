using System;

namespace Cinema.Persisted.Entities
{
    public class Place : BaseEntity
    {
        public int Raw { get; set; }

        public int Number { get; set; }

        public Guid? HallId { get; set; }

        public virtual Hall Hall { get; set; }

        public Guid? TicketId { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
