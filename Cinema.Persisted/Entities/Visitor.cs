using System;

namespace Cinema.Persisted.Entities
{
    public class Visitor : BaseEntity
    {
        public string Name { get; set; }

        public Guid? TicketId { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
