using Cinema.BusinessLogic.Interfaces;
using Cinema.Persisted.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cinema.Web.Controllers
{
    [Route("api/film")]
    public class FilmController : Controller
    {
        private readonly IFilmService _filmService;

        public FilmController(IFilmService filmService)
        {
            _filmService = filmService;
        }

        /// <summary>
        /// Get specific film.
        /// </summary>
        /// <param name="filmId">Film Id.</param>
        /// <returns>Film by id.</returns>
        /// <response code="404">If film is not found.</response>
        [HttpGet]
        [Route("{filmId}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(Guid filmId)
        {
            var film = await _filmService.GetAsync(filmId);

            return Ok(film);
        }

        /// <summary>
        /// Get all films.
        /// </summary>
        /// <returns>All films.</returns>
        /// <response code="404">If films are not found.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll()
        {
            var films = await _filmService.GetAllAsync();

            return Ok(films);
        }

        /// <summary>
        /// Add new film.
        /// </summary>
        /// <param name="film">Film.</param>
        /// <returns>A newly created film.</returns>
        /// <response code="200">Returns the newly created film.</response>
        /// <response code="400">If request data is null.</response>
        [HttpPost]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post(Film film)
        {
            var createdFilm = await _filmService.AddAsync(film);

            return Ok(createdFilm);
        }

        /// <summary>
        /// Update film.
        /// </summary>
        /// <param name="filmId">Film Id.</param>
        /// <returns>Updated film.</returns>
        /// <response code="200">Returns the updated film.</response>
        /// <response code="400">If request data is null.</response>
        /// <response code="404">If film is not found.</response>
        [HttpPut]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Route("{filmId}")]
        public async Task<IActionResult> Put(Guid filmId, Film film)
        {
            var updatedFilm = await _filmService.UpdateAsync(filmId, film);
            return Ok(updatedFilm);
        }

        /// <summary>
        /// Delete film.
        /// </summary>
        /// <param name="filmId">Film Id.</param>
        /// <returns></returns>
        /// <response code="204">Returns no content status code.</response>
        /// <response code="404">If film is not found.</response>
        [HttpDelete]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        [Route("{filmId}")]
        public async Task<IActionResult> Delete(Guid filmId)
        {
            await _filmService.RemoveAsync(filmId);
            return NoContent();
        }
    }
}