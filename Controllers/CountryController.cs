using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICountryInterface _contryinterface;
        public CountryController(IMapper mapper, ICountryInterface countryInterface)
        {
            this._mapper = mapper;
            this._contryinterface = countryInterface;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<List<Country>>))]
        public IActionResult GetContries()
        {
            var contries = _mapper.Map<List<CountryDto>>(_contryinterface.GetContries());

            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            return Ok(contries);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        [ProducesResponseType(400)]
        public IActionResult GetCountryById(int id)
        {
            if (!_contryinterface.ContryExist(id))
            {
                return BadRequest();
            }

            var country = _mapper.Map<CountryDto>(_contryinterface.GetCountryById(id));

            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            return Ok(country);
        }

        [HttpGet("/owners/{ownerId}")]
        [ProducesResponseType (200, Type = typeof(IEnumerable<Country>))]
        [ProducesResponseType (400)]
        public IActionResult GetContryByOwnerId(int ownerId)
        {
            var contries = _mapper.Map<List<CountryDto>>(_contryinterface.GetCountryByOwner(ownerId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(contries);
        }


    }
}
