using Cinema.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Web.Providers.Interfaces
{
    public interface IReservationProvider
    {
        Task<IEnumerable<ReservationShortRecordModel>> GetAllAsync();

        Task<ReservationRecordModel> GetAsync(Guid id);

        Task<Guid> PostAsync(ReservationModel reservation);

        Task<ReservationRecordModel> ReservationPay(Guid id);

        Task<ReservationRecordModel> ReservationCancel(Guid id);
    }
}
