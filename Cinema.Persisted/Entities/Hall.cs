using System.Collections.Generic;

namespace Cinema.Persisted.Entities
{
    public class Hall : BaseEntity
    {
        public int Number { get; set; }

        public int PlaceCount { get; set; }

        public int RawCount { get; set; }

        public virtual ICollection<Place> Places { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
}
