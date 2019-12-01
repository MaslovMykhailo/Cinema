using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Web.Models
{
    public class ReservationRecordModel : ReservationShortRecordModel
    {
        public DateTime ReservationTime { get; set; }

        public DateTime PaymentTime { get; set; }

        public DateTime CancelationTime { get; set; }

        public IEnumerable<Guid> TicketIds { get; set; }
    }
}
