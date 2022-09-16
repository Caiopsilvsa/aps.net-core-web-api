using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController:Controller
    {
        private readonly IMapper _mapper;
        private readonly IReviewInterface _reviewInterface;
        public ReviewController(IReviewInterface reviewInterface, IMapper mapper)
        {
            this._mapper = mapper;
            this._reviewInterface = reviewInterface;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(this._reviewInterface.GetReviews());

            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewById(int reviewId)
        {
            if (!_reviewInterface.ReviewExist(reviewId))
            {
                return NotFound();
            }

            var review = _mapper.Map<ReviewDto>(_reviewInterface.GetReviewById(reviewId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(review);
        }

        [HttpGet("/pokemon/review/{pokemonId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewByPokemonId(int pokemonId)
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewInterface.GetReviewsByAPokemonId(pokemonId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(reviews);
        }
    }
}
