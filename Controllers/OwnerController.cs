using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController:Controller
    {
        private readonly IOwnerInterface _ownerInterface;
        private readonly ICountryInterface _countryInterface;
        private readonly IMapper _mapper;
        public OwnerController(IOwnerInterface ownerInterface, ICountryInterface countryInterface, IMapper mapper)
        {
            this._ownerInterface = ownerInterface;
            this._countryInterface = countryInterface;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult GetAllOwners()
        {
            var owners = _mapper.Map<List<OwnerDto>>(_ownerInterface.GetOwners());

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(owners);
        }

        [HttpGet("/owner/{ownerId}")]
        [ProducesResponseType(200, Type = typeof( Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetOwnerById(int ownerId)
        {
            if (!_ownerInterface.OwnerExist(ownerId))
            {
                return NotFound();
            }

            var owner = _mapper.Map<OwnerDto>(_ownerInterface.GetOwnerById(ownerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(owner);
        }

        [HttpGet("/pokemon/{ownerId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByOwner(int ownerId)
        {
            if (!_ownerInterface.OwnerExist(ownerId))
            {
                return NotFound();
            }

            var pokemon = _mapper.Map<List<PokemonDto>>(_ownerInterface.GetPokemonByOwner(ownerId)) ;
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pokemon);

        }

        [HttpPost]
        [ProducesResponseType(204, Type = typeof(IEnumerable<Owner>))]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner([FromBody] OwnerDto ownerCreate, [FromQuery] int countryId)
        {
            if(ownerCreate == null)
            {
                return BadRequest(ModelState);
            }

            var ownerTest = _ownerInterface.GetOwners()
                .Where(o => o.Name.Trim().ToUpper() == ownerCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if(ownerTest != null)
            {
                ModelState.AddModelError("", "Owner já existente");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mappedOwner = _mapper.Map<Owner>(ownerCreate);
                mappedOwner.Country = _countryInterface.GetCountryById(countryId);

            if (!_ownerInterface.CreateOwner(mappedOwner))
            {
                ModelState.AddModelError("", "Um erro ocorreu durante o processamento");
                return StatusCode(500, ModelState);
            }

            return Ok("Owner criado com sucesso!");

        }

        [HttpPut("{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOwner([FromBody] OwnerDto owner, int ownerId)
        {
            if(owner == null)
            {
                return BadRequest(ModelState);
            }

            if(owner.Id != ownerId)
            {
                return BadRequest();
            }

            if (!_ownerInterface.OwnerExist(ownerId))
            {
                ModelState.AddModelError("", "Owner já cadastrado");
                return StatusCode(400, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return NotFound(ModelState);
            }

            var mappedOwner = _mapper.Map<Owner>(owner);

            if (!_ownerInterface.UpdateOwner(mappedOwner))
            {
                ModelState.AddModelError("", "ocorreu um erro durante a operação");
                return StatusCode(500,ModelState);
            }

            return NoContent();
        }
    }
}
