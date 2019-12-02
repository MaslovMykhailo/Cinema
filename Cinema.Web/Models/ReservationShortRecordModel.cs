using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Web.Models
{
    public class ReservationShortRecordModel
    {
        public Guid Id { get; set; }

        public bool WasPaid { get; set; }

        public bool WasCanceled { get; set; }

        public Guid VisitorId { get; set; }
    }
}
