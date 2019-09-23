using System;

namespace Cinema.Web.Models
{
    public class SessionModel
    {
        public Guid FilmId { get; set; }

        public DateTime Time { get; set; }

        public Guid HallId { get; set; }

        public int TicketsSaled { get; set; }
    }
}
