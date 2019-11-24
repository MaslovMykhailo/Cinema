using AutoMapper;
using Cinema.Web.Models;
using Cinema.Web.Providers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Web.Controllers
{
    [Route("api/film")]
    public class FilmController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IFilmProvider _filmProvider;        

        public FilmController(IMapper mapper, IFilmProvider filmProvider)
        {
            _mapper = mapper;
            _filmProvider = filmProvider;            
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
            var films = _mapper.Map<List<FilmModel>>(await _filmProvider.GetBySearchModelAsync(filmModel));
            return Ok(films);
        }

        /// <summary>
        /// Get all cached films by search query.
        /// </summary>
        /// <returns>All cached films by search query.</returns>
        /// <response code="404">If films are not found.</response>
        [HttpGet]
        [Route("cached-search")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllBySearchQueryCached([FromQuery] FilmSearchModel filmModel)
        {
            var films = _mapper.Map<List<FilmModel>>(await _filmProvider.GetBySearchModelCachedAsync(filmModel));
            return Ok(films);
        }
    }
}