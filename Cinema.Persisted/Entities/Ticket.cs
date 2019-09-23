using System;

namespace Cinema.Persisted.Entities
{
    public class Ticket : BaseEntity
    {
        public int Number { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public Guid? VisitorId { get; set; }

        public virtual Visitor Visitor { get; set; }

        public Guid? PlaceId { get; set; }

        public virtual Place Place { get; set; }

        public Guid? FilmId { get; set; }

        public virtual Film Film { get; set; }
    }
}
