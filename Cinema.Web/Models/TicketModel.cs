using System;

namespace Cinema.Web.Models
{
    public class TicketModel
    {
        public int Number { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }
        
        public Guid PlaceId { get; set; }

        public Guid FilmId { get; set; }
    }
}
