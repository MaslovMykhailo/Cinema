using AutoMapper;
using Cinema.BusinessLogic.Filtering;
using Cinema.BusinessLogic.Interfaces;
using Cinema.Persisted.Entities;
using Cinema.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cinema.Web.Controllers
{
    [Route("api/film")]
    public class FilmController : Controller
    {
        private readonly IFilmService _filmService;
        private readonly IMapper _mapper;

        public FilmController(IFilmService filmService, IMapper mapper)
        {
            _filmService = filmService;
            _mapper = mapper;
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
        /// Get all films by duration time.
        /// </summary>
        /// <param name="duration">Film duration time.</param>
        /// <returns>All films by duration time.</returns>
        /// <response code="404">If films are not found.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        [Route("duration/{duration}")]
        public async Task<IActionResult> GetAllByDurationTime(float duration)
        {
            var films = await _filmService.Find(FilmSpecification.Duration(duration).IsSatisfiedBy());

            return Ok(films);
        }

        /// <summary>
        /// Get all films by name.
        /// </summary>
        /// <param name="filmName">Film name.</param>
        /// <returns>All films by name.</returns>
        /// <response code="404">If films are not found.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        [Route("name/{filmName}")]
        public async Task<IActionResult> GetAllByName(string filmName)
        {
            var films = await _filmService.Find(FilmSpecification.Name(filmName).IsSatisfiedBy());

            return Ok(films);
        }

        /// <summary>
        /// Get all films by filmmaker.
        /// </summary>
        /// <param name="filmmaker">Filmmaker.</param>
        /// <returns>All films by filmmaker.</returns>
        /// <response code="404">If films are not found.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        [Route("filmmaker/{filmmaker}")]
        public async Task<IActionResult> GetAllByFilmmaker(string filmmaker)
        {
            var films = await _filmService.Find(FilmSpecification.Filmmaker(filmmaker).IsSatisfiedBy());

            return Ok(films);
        }

        /// <summary>
        /// Add new film.
        /// </summary>
        /// <param name="filmModel">Film.</param>
        /// <returns>A newly created film.</returns>
        /// <response code="200">Returns the newly created film.</response>
        /// <response code="400">If request data is null.</response>
        [HttpPost]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post(FilmModel filmModel)
        {

            var film = _mapper.Map<Film>(filmModel);
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
        public async Task<IActionResult> Put(Guid filmId, FilmModel filmModel)
        {
            var film = await _filmService.GetAsync(filmId);
            _mapper.Map(filmModel, film);
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