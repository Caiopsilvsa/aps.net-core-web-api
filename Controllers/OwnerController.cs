using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PokemonReviewApp.Data;
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
        private readonly IMapper _mapper;
        public OwnerController(IOwnerInterface ownerInterface, IMapper mapper)
        {
            this._ownerInterface = ownerInterface;
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

    }
}
