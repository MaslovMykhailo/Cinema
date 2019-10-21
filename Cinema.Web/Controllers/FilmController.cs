using AutoMapper;
using Cinema.BusinessLogic.Filtering;
using Cinema.BusinessLogic.Interfaces;
using Cinema.Persisted.Entities;
using Cinema.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Cinema.CinemaSearcher.Client;
using System.Collections.Generic;

namespace Cinema.Web.Controllers
{
    [Route("api/film")]
    public class FilmController : Controller
    {
        private readonly ICinemaSearcherClient _client;
        private readonly IMapper _mapper;

        public FilmController(IFilmService filmService, IMapper mapper, ICinemaSearcherClient client)
        {
            _mapper = mapper;
            _client = client;
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
            var films = await _client.GetAsync<Film>();

            return Ok(films);
        }

        /// <summary>
        /// Get all films by search query.
        /// </summary>
        /// <returns>All films by search query.</returns>
        /// <response code="404">If films are not found.</response>
        [HttpGet]
        [Route("/api/film/search")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllBySearchQuery([FromQuery] FilmSearchModel filmModel)
        {
            IEnumerable<Film> films = await _client.GetBySearchQueryAsync<Film>(Request.QueryString);
            return Ok(_mapper.Map<List<FilmModel>>(films));
        }
    }
}