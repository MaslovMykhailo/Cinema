﻿using AutoMapper;
using Cinema.BusinessLogic.Interfaces;
using Cinema.Persisted.Entities;
using Cinema.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cinema.Web.Controllers
{
    [Route("api/visitor")]
    public class VisitorController : Controller
    {
        private readonly IVisitorService _visitorService;
        private readonly IMapper _mapper;

        public VisitorController(IVisitorService visitorService, IMapper mapper)
        {
            _visitorService = visitorService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get specific visitor.
        /// </summary>
        /// <param name="visitorId">Place Id.</param>
        /// <returns>Place by id.</returns>
        /// <response code="404">If visitor is not found.</response>
        [HttpGet]
        [Route("{visitorId}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(Guid visitorId)
        {
            var ticket = await _visitorService.GetAsync(visitorId);

            return Ok(ticket);
        }

        /// <summary>
        /// Get all visitors.
        /// </summary>
        /// <returns>All visitors.</returns>
        /// <response code="404">If visitors are not found.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll()
        {
            var tickets = await _visitorService.GetAllAsync();

            return Ok(tickets);
        }

        /// <summary>
        /// Add new visitor.
        /// </summary>
        /// <param name="visitorModel">Visitor.</param>
        /// <returns>A newly created visitor.</returns>
        /// <response code="200">Returns the newly created visitor.</response>
        /// <response code="400">If request data is null.</response>
        [HttpPost]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post(VisitorModel visitorModel)
        {
            var visitor = _mapper.Map<Visitor>(visitorModel);
            var createdTicket = await _visitorService.AddAsync(visitor);

            return Ok(createdTicket);
        }

        /// <summary>
        /// Update visitor.
        /// </summary>
        /// <param name="visitorId">Visitor Id.</param>
        /// <returns>Updated visitor.</returns>
        /// <response code="200">Returns the updated visitor.</response>
        /// <response code="400">If request data is null.</response>
        /// <response code="404">If visitor is not found.</response>
        [HttpPut]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Route("{visitorId}")]
        public async Task<IActionResult> Put(Guid visitorId, VisitorModel visitorModel)
        {
            var visitor = await _visitorService.GetAsync(visitorId);
            _mapper.Map(visitorModel, visitor);
            var updatedPlace = await _visitorService.UpdateAsync(visitorId, visitor);

            return Ok(updatedPlace);
        }

        /// <summary>
        /// Delete visitor.
        /// </summary>
        /// <param name="visitorId">Visitor Id.</param>
        /// <returns></returns>
        /// <response code="204">Returns no content status code.</response>
        /// <response code="404">If visitor is not found.</response>
        [HttpDelete]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        [Route("{visitorId}")]
        public async Task<IActionResult> Delete(Guid visitorId)
        {
            await _visitorService.RemoveAsync(visitorId);
            return NoContent();
        }
    }
}