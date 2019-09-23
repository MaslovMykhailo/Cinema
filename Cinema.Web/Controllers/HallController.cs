using AutoMapper;
using Cinema.BusinessLogic.Interfaces;
using Cinema.Persisted.Entities;
using Cinema.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cinema.Web.Controllers
{
    [Route("api/hall")]
    public class HallController : Controller
    {
        private readonly IHallService _hallService;
        private readonly IMapper _mapper;

        public HallController(IHallService hallService, IMapper mapper)
        {
            _hallService = hallService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get specific hall.
        /// </summary>
        /// <param name="filmId">Hall Id.</param>
        /// <returns>Hall by id.</returns>
        /// <response code="404">If hall is not found.</response>
        [HttpGet]
        [Route("{hallId}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(Guid hallId)
        {
            var hall = await _hallService.GetAsync(hallId);

            return Ok(hall);
        }

        /// <summary>
        /// Get all halls.
        /// </summary>
        /// <returns>All halls.</returns>
        /// <response code="404">If halls are not found.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll()
        {
            var halls = await _hallService.GetAllAsync();

            return Ok(halls);
        }

        /// <summary>
        /// Add new hall.
        /// </summary>
        /// <param name="hallModel">Hall.</param>
        /// <returns>A newly created hall.</returns>
        /// <response code="200">Returns the newly created hall.</response>
        /// <response code="400">If request data is null.</response>
        [HttpPost]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post(HallModel hallModel)
        {
            var hall = _mapper.Map<Hall>(hallModel);
            var createdHall = await _hallService.AddAsync(hall);

            return Ok(createdHall);
        }

        /// <summary>
        /// Update hall.
        /// </summary>
        /// <param name="hallId">Hall Id.</param>
        /// <returns>Updated hall.</returns>
        /// <response code="200">Returns the updated hall.</response>
        /// <response code="400">If request data is null.</response>
        /// <response code="404">If hall is not found.</response>
        [HttpPut]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Route("{hallId}")]
        public async Task<IActionResult> Put(Guid hallId, HallModel hallModel)
        {
            var hall = await _hallService.GetAsync(hallId);
            _mapper.Map(hallModel, hall);
            var updatedHall = await _hallService.UpdateAsync(hallId, hall);
            return Ok(updatedHall);
        }

        /// <summary>
        /// Delete hall.
        /// </summary>
        /// <param name="hallId">Hall Id.</param>
        /// <returns></returns>
        /// <response code="204">Returns no content status code.</response>
        /// <response code="404">If hall is not found.</response>
        [HttpDelete]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        [Route("{hallId}")]
        public async Task<IActionResult> Delete(Guid hallId)
        {
            await _hallService.RemoveAsync(hallId);
            return NoContent();
        }
    }
}