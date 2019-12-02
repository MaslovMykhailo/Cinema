using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Web.Models
{
    public class ReservationModel
    {
        public IEnumerable<Guid> TicketIds { get; set; }

        public Guid VisitorId { get; set; }
    }
}
