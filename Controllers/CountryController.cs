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
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        [ProducesResponseType(400)]
        public IActionResult GetContryByOwnerId(int ownerId)
        {
            var contries = _mapper.Map<List<CountryDto>>(_contryinterface.GetCountryByOwner(ownerId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(contries);
        }

        [HttpPost]
        [ProducesResponseType(204, Type = typeof(IEnumerable<Country>))]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry([FromBody] CountryDto countryCreate)
        {
            if (countryCreate == null)
            {
                return BadRequest();
            }

            var countryTest = _contryinterface.GetContries()
                .Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.ToUpper()).FirstOrDefault();

            if(countryTest != null)
            {
                ModelState.AddModelError("", "Esse país já existe");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryMapped = _mapper.Map<Country>(countryCreate);

            if (!_contryinterface.CreateCountry(countryMapped))
            {
                ModelState.AddModelError("", "Um erro ocorru durante a operação");
                return StatusCode(500, ModelState);
            }

            return Ok("Country criado com sucesso!");


            
        }

      

    }
}
