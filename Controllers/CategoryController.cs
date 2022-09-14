using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using System.Collections.Generic;

namespace PokemonReviewApp.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryInterface _categoryInterface;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryInterface categoryInterface, IMapper mapper) {
            this._categoryInterface = categoryInterface;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryInterface.GetCategories());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(categories);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult getCategoryById(int id)
        {
            if (!this._categoryInterface.CategoryExist(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var category = _mapper.Map<CategoryDto>(_categoryInterface.GetCategoryById(id));
            return Ok(category);
        }

        [HttpGet("pokemon/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByCategory(int categoryId)
        {
          
            var pokemonByCategory = _mapper.Map<List<PokemonDto>>(_categoryInterface.GetPokemonByCategory(categoryId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(pokemonByCategory);
        }
    }
}
