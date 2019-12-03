using System;
using System.Threading.Tasks;
using Cinema.Web.Models;
using Cinema.Web.Providers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Web.Controllers
{
    [Route("api/reservation")]
    public class ReservationController : Controller
    {
        private readonly IReservationProvider _reservationProvider;

        public ReservationController(IReservationProvider reservationProvider)
        {
            _reservationProvider = reservationProvider;
        }

        [HttpGet("{reservationId}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(Guid reservationId)
        {
            return Ok(await _reservationProvider.GetAsync(reservationId));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _reservationProvider.GetAllAsync());
        }

        [HttpPost]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody]ReservationModel model)
        {
            return Ok(await _reservationProvider.PostAsync(model));
        }

        [HttpPut]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Route("payment/{reservationId}")]
        public async Task<IActionResult> PutPayment(Guid reservationId)
        {
            return Ok(await _reservationProvider.ReservationPay(reservationId));
        }

        [HttpPut]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Route("cancelation/{reservationId}")]
        public async Task<IActionResult> PutCancelation(Guid reservationId)
        {
            return Ok(await _reservationProvider.ReservationCancel(reservationId));
        }
    }
}