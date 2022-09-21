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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if(categoryCreate == null)
            {
                return BadRequest();
            }

            var category = _categoryInterface.GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if(category != null)
            {
                ModelState.AddModelError("", "Essa catgoria já existe");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryMap = _mapper.Map<Category>(categoryCreate);

            if (!_categoryInterface.SaveCategory(categoryMap))
            {
                ModelState.AddModelError("", "Algo de errado occoreu durante o processo");
                return StatusCode (500,ModelState);
            }

            return Ok("Categoria criada com sucesso");

        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCategory([FromBody]CategoryDto category, int categoryId)
        {
            if(category == null)
            {
                return BadRequest(ModelState);
            }

            if(categoryId != category.Id)
            {
                return BadRequest();
            }

            if (!_categoryInterface.CategoryExist(categoryId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var categoryMapped = _mapper.Map<Category>(category);

            if (!_categoryInterface.UpdateCategory(categoryMapped))
            {
                ModelState.AddModelError("", "Houve um erro durante a operação");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_categoryInterface.CategoryExist(categoryId))
            {
                return NotFound();
            }

            var categoryToDelete = _categoryInterface.GetCategoryById(categoryId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_categoryInterface.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", "Houve um erro durante a operação");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
