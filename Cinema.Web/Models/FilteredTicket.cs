using Cinema.Persisted.Entities;

namespace Cinema.Web.Models
{
    public class FilteredTicket
    {
        public decimal Price { get; set; }

        public int PlaceNumber { get; set; }

        public Film Film { get; set; }
    }
}
