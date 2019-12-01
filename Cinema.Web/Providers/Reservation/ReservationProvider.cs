using Cinema.Web.Clients;
using Cinema.Web.Models;
using Cinema.Web.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Web.Providers.Reservation
{
    public class ReservationProvider : IReservationProvider
    {
        private readonly IReservationClient _client;

        public ReservationProvider(IReservationClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<ReservationShortRecordModel>> GetAllAsync()
        {
            return await _client.GetAll<ReservationShortRecordModel>();
        }

        public async Task<ReservationRecordModel> GetAsync(Guid id)
        {
            return await _client.Get<ReservationRecordModel>(id);
        }

        public async Task<Guid> PostAsync(ReservationModel reservation)
        {
            return await _client.Post<Guid>(reservation);
        }

        public async Task<ReservationRecordModel> ReservationPay(Guid id)
        {
            return await _client.ReservationPay<ReservationRecordModel>(id);
        }

        public async Task<ReservationRecordModel> ReservationCancel(Guid id)
        {
            return await _client.ReservationCancel<ReservationRecordModel>(id);
        }
    }
}
