using AutoMapper;
using Cinema.BusinessLogic.Interfaces;
using Cinema.Persisted.Entities;
using Cinema.Web.Clients;
using Cinema.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Web.Controllers
{
    [Route("api/film")]
    public class FilmController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICinemaSearcherClient _searcherClient;
        private readonly ICinemaExplorerClient _explorerClient;

        public FilmController(IMapper mapper, ICinemaSearcherClient searcherClient, ICinemaExplorerClient explorerClient)
        {
            _mapper = mapper;
            _searcherClient = searcherClient;
            _explorerClient = explorerClient;
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
            var films = await _searcherClient.GetAsync<Film>();

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
            IEnumerable<Film> films = await _searcherClient.GetBySearchQueryAsync<Film>(filmModel);
            return Ok(_mapper.Map<List<FilmModel>>(films));
        }

        /// <summary>
        /// Get films price list.
        /// </summary>
        /// <returns>Films price list.</returns>
        /// <response code="404">If films are not found.</response>
        [HttpGet]
        [Route("/api/film/price-list")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPriseList()
        {
            IEnumerable<Film> films = await _explorerClient.GetAsync<Film>();
            return Ok(_mapper.Map<List<FilmModel>>(films));
        }
    }
}