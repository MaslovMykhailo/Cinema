using System.Collections.Generic;

namespace Cinema.Persisted.Entities
{
    public class Film : BaseEntity
    {
        public string Name { get; set; }

        public int DurationTime { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
