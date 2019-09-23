using System.Collections.Generic;

namespace Cinema.Persisted.Entities
{
    public class Film : BaseEntity
    {
        public string Name { get; set; }

        public int DurationTime { get; set; }

        public string Filmmaker { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
}
