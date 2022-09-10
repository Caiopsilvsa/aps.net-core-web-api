using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonInterface _pokemonRepository;
        private readonly IMapper _mapper;
        public PokemonController(IPokemonInterface pokemonRepository, IMapper imapper) {
            this._pokemonRepository = pokemonRepository;
            this._mapper = imapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemon = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };
            return Ok(pokemon);
        }

        [HttpGet ("{id}")]
        [ProducesResponseType(200, Type = typeof (Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonById(int id)
        {
            if (!_pokemonRepository.PokemonExists(id))
            {
                return NotFound();
            }

            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemonById(id));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(pokemon);
        }

        [HttpGet("{name}/pokename")]
        [ProducesResponseType(200,Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByName(string name)
        {
            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemonByName(name));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(pokemon);
        }
    }
}
