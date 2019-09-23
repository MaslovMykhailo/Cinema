using AutoMapper;
using Cinema.BusinessLogic.Interfaces;
using Cinema.Persisted.Entities;
using Cinema.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cinema.Web.Controllers
{
    [Route("api/session")]
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        public SessionController(ISessionService sessionService, IMapper mapper)
        {
            _sessionService = sessionService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get specific session.
        /// </summary>
        /// <param name="sessionId">Session Id.</param>
        /// <returns>Session by id.</returns>
        /// <response code="404">If session is not found.</response>
        [HttpGet]
        [Route("{sessionId}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(Guid sessionId)
        {
            var session = await _sessionService.GetAsync(sessionId);

            return Ok(session);
        }

        /// <summary>
        /// Get all sessions.
        /// </summary>
        /// <returns>All sessions.</returns>
        /// <response code="404">If sessions are not found.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll()
        {
            var sessions = await _sessionService.GetAllAsync();

            return Ok(sessions);
        }

        /// <summary>
        /// Add new session.
        /// </summary>
        /// <param name="sessionModel">Session.</param>
        /// <returns>A newly created session.</returns>
        /// <response code="200">Returns the newly created session.</response>
        /// <response code="400">If request data is null.</response>
        [HttpPost]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post(SessionModel sessionModel)
        {
            var session = _mapper.Map<Session>(sessionModel);
            var createdSession = await _sessionService.AddAsync(session);

            return Ok(createdSession);
        }

        /// <summary>
        /// Update session.
        /// </summary>
        /// <param name="sessionId">Session Id.</param>
        /// <returns>Updated session.</returns>
        /// <response code="200">Returns the updated session.</response>
        /// <response code="400">If request data is null.</response>
        /// <response code="404">If session is not found.</response>
        [HttpPut]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Route("{sessionId}")]
        public async Task<IActionResult> Put(Guid sessionId, SessionModel sessionModel)
        {
            var session = await _sessionService.GetAsync(sessionId);
            _mapper.Map(sessionModel, session);
            var updatedSession = await _sessionService.UpdateAsync(sessionId, session);
            return Ok(updatedSession);
        }

        /// <summary>
        /// Delete film.
        /// </summary>
        /// <param name="sessionId">Session Id.</param>
        /// <returns></returns>
        /// <response code="204">Returns no content status code.</response>
        /// <response code="404">If film is not found.</response>
        [HttpDelete]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        [Route("{filmId}")]
        public async Task<IActionResult> Delete(Guid sessionId)
        {
            await _sessionService.RemoveAsync(sessionId);
            return NoContent();
        }
    }
}