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

        [HttpPost]
        [ProducesResponseType(204, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromBody]PokemonDto pokemon,[FromQuery] int ownerId, int categoryId)
        {
            if(pokemon == null)
            {
                return BadRequest(ModelState);
            }

            var pokemonTest = _pokemonRepository.GetPokemonByName(pokemon.Name.Trim().ToUpper());
                if(pokemonTest != null)
                {
                    ModelState.AddModelError("", "Pokemon já existe");
                    return StatusCode(422, ModelState);
                }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mappedPokemon = _mapper.Map<Pokemon>(pokemon);

            if(!_pokemonRepository.CreatePokemon(mappedPokemon, ownerId, categoryId))
            {
                ModelState.AddModelError("", "Hove um problema durante a operação");
                return StatusCode(500, ModelState);
            }
            
            return Ok("Pokemon Criado com sucesso!");
        }

        [HttpPut("{pokeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePokemon(
            [FromBody]PokemonDto pokemon,
            int pokeId,
            [FromQuery] int categoryId, int ownerId)
        {
            if(pokemon == null)
            {
                return BadRequest(ModelState);
            }

            if(pokeId != pokemon.Id)
            {
                return BadRequest();
            }

            if (!_pokemonRepository.PokemonExists(pokeId))
            {
                return NotFound(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return NotFound(ModelState);
            }

            var mappedPokemon = _mapper.Map<Pokemon>(pokemon);

            if(!_pokemonRepository.UpdatePokemon(mappedPokemon, categoryId, ownerId))
            {
                ModelState.AddModelError("", "Houve um erro durante a operação");    
            }

            return NoContent();
        }
    }
}
