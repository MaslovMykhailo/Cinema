using AutoMapper;
using Cinema.CinemaSearcher.Client;
using Cinema.Persisted.Entities;
using Cinema.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cinema.Web.Controllers
{
    [Route("api/ticket")]
    public class TicketController : Controller
    {
        private readonly BusinessLogic.Interfaces.ITicketService _ticketService;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ICinemaSearcherClient _cinemaSearcherClient;

        public TicketController(BusinessLogic.Interfaces.ITicketService ticketService, IMapper mapper, IHttpClientFactory clientFactory, ICinemaSearcherClient cinemaSearcherClient)
        {
            _ticketService = ticketService;
            _mapper = mapper;
            _clientFactory = clientFactory;
            _cinemaSearcherClient = cinemaSearcherClient;
        }

        /// <summary>
        /// Get specific ticket.
        /// </summary>
        /// <param name="ticketId">Ticket Id.</param>
        /// <returns>Place by id.</returns>
        /// <response code="404">If ticket is not found.</response>
        [HttpGet]
        [Route("{ticketId}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(Guid ticketId)
        {
            var ticket = await _ticketService.GetAsync(ticketId);

            return Ok(ticket);
        }

        /// <summary>
        /// Get all tickets.
        /// </summary>
        /// <returns>All tickets.</returns>
        /// <response code="404">If tickets are not found.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll()
        {
            var tickets = await _ticketService.GetAllAsync();

            return Ok((tickets));
        }

        /// <summary>
        /// Get all tickets.
        /// </summary>
        /// <returns>All tickets.</returns>
        /// <response code="404">If tickets are not found.</response>
        [HttpGet]
        [Route("/price-list")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPriceList()
        {
            var tickets = await _ticketService.GetAllAsync();
            var filteredTickets = _mapper.Map<IEnumerable<Ticket>, List<FilteredTicket>>(tickets);

            return Ok(filteredTickets);
        }

        /// <summary>
        /// Add new ticket.
        /// </summary>
        /// <param name="ticket">Ticket.</param>
        /// <returns>A newly created ticket.</returns>
        /// <response code="200">Returns the newly created ticket.</response>
        /// <response code="400">If request data is null.</response>
        [HttpPost]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post(TicketModel ticketModel)
        {
            var ticket = _mapper.Map<Ticket>(ticketModel);
            var createdTicket = await _ticketService.AddAsync(ticket);

            return Ok(createdTicket);
        }

        /// <summary>
        /// Update ticket.
        /// </summary>
        /// <param name="ticketId">Ticket Id.</param>
        /// <returns>Updated ticket.</returns>
        /// <response code="200">Returns the updated ticket.</response>
        /// <response code="400">If request data is null.</response>
        /// <response code="404">If ticket is not found.</response>
        [HttpPut]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Route("{ticketId}")]
        public async Task<IActionResult> Put(Guid ticketId, TicketModel ticketModel)
        {
            var ticket = await _ticketService.GetAsync(ticketId);
            _mapper.Map(ticketModel, ticket);
            var updatedPlace = await _ticketService.UpdateAsync(ticketId, ticket);
            return Ok(updatedPlace);
        }

        /// <summary>
        /// Delete ticket.
        /// </summary>
        /// <param name="ticketId">Ticket Id.</param>
        /// <returns></returns>
        /// <response code="204">Returns no content status code.</response>
        /// <response code="404">If ticket is not found.</response>
        [HttpDelete]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        [Route("{ticketId}")]
        public async Task<IActionResult> Delete(Guid ticketId)
        {
            await _ticketService.RemoveAsync(ticketId);
            return NoContent();
        }
    }
}