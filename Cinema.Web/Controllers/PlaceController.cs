using Cinema.BusinessLogic.Interfaces;
using Cinema.Persisted.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cinema.Web.Controllers
{
    [Route("api/place")]
    public class PlaceController : Controller
    {
        private readonly IPlaceService _placeService;

        public PlaceController(IPlaceService placeService)
        {
            _placeService = placeService;
        }

        /// <summary>
        /// Get specific place.
        /// </summary>
        /// <param name="placeId">Place Id.</param>
        /// <returns>Place by id.</returns>
        /// <response code="404">If place is not found.</response>
        [HttpGet]
        [Route("{placeId}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(Guid placeId)
        {
            var place = await _placeService.GetAsync(placeId);

            return Ok(place);
        }

        /// <summary>
        /// Get all places.
        /// </summary>
        /// <returns>All places.</returns>
        /// <response code="404">If places are not found.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll()
        {
            var places = await _placeService.GetAllAsync();

            return Ok(places);
        }

        /// <summary>
        /// Add new place.
        /// </summary>
        /// <param name="place">Place.</param>
        /// <returns>A newly created place.</returns>
        /// <response code="200">Returns the newly created place.</response>
        /// <response code="400">If request data is null.</response>
        [HttpPost]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post(Place place)
        {
            var createdPlace = await _placeService.AddAsync(place);

            return Ok(createdPlace);
        }

        /// <summary>
        /// Update place.
        /// </summary>
        /// <param name="placeId">Place Id.</param>
        /// <returns>Updated place.</returns>
        /// <response code="200">Returns the updated place.</response>
        /// <response code="400">If request data is null.</response>
        /// <response code="404">If place is not found.</response>
        [HttpPut]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Route("{placeId}")]
        public async Task<IActionResult> Put(Guid placeId, Place place)
        {
            var updatedPlace = await _placeService.UpdateAsync(placeId, place);
            return Ok(updatedPlace);
        }

        /// <summary>
        /// Delete place.
        /// </summary>
        /// <param name="placeId">Place Id.</param>
        /// <returns></returns>
        /// <response code="204">Returns no content status code.</response>
        /// <response code="404">If place is not found.</response>
        [HttpDelete]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        [Route("{placeId}")]
        public async Task<IActionResult> Delete(Guid placeId)
        {
            await _placeService.RemoveAsync(placeId);
            return NoContent();
        }
    }
}