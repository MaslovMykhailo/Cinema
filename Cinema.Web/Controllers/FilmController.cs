using AutoMapper;
using Cinema.Persisted.Entities;
using Cinema.Web.Clients;
using Cinema.Web.Models;
using Cinema.Web.Providers.FilmProviders;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Web.Controllers
{
    [Route("api/film")]
    public class FilmController : Controller
    {
        private readonly IMapper _mapper;
        private readonly FilmProvider _filmProvider;

        public FilmController(IMapper mapper, ICinemaSearcherClient searcherClient, ICinemaExplorerClient explorerClient)
        {
            _mapper = mapper;
            _filmProvider = new FilmProvider(searcherClient, explorerClient);
        }

        /// <summary>
        /// Get all films by search query.
        /// </summary>
        /// <returns>All films by search query.</returns>
        /// <response code="404">If films are not found.</response>
        [HttpGet]
        [Route("search")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllBySearchQuery([FromQuery] FilmSearchModel filmModel)
        {
            List<Film> films = await _filmProvider.GetBySearchModelAsync(filmModel);
            return Ok(_mapper.Map<List<FilmModel>>(films));
        }
    }
}