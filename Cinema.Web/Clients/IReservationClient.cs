using Cinema.Web.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Cinema.Web.Clients
{
    public interface IReservationClient
    {
        [Get("/api/reservation")]
        Task<IEnumerable<T>> GetAll<T>();

        [Get("/api/reservation/{id}")]
        Task<T> Get<T>(Guid id);

        [Post("/api/reservation")]
        Task<T> Post<T>([Body] ReservationModel reservation);

        [Put("/api/reservation/payment/{id}")]
        Task<T> ReservationPay<T>(Guid id);

        [Put("/api/reservation/cancelation/{id}")]
        Task<T> ReservationCancel<T>(Guid id);
    }
}
