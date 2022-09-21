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
        private readonly IPokemonInterface _pokemonInterface;
        private readonly IReviewerInterface _reviewerInterface;

        public ReviewController
            (
            IReviewInterface reviewInterface, 
            IPokemonInterface pokemonInterface,
            IReviewerInterface reviewerInterface,
            IMapper mapper
            )
        {
            this._mapper = mapper;
            this._reviewInterface = reviewInterface;
            this._reviewerInterface = reviewerInterface;
            this._pokemonInterface = pokemonInterface;
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

        [HttpPost]
        [ProducesResponseType(204, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromBody]ReviewDto review, [FromQuery] int pokeId,int reviewerId )
        {
            if(review == null)
            {
                return BadRequest(ModelState);
            }

            var testReview = _reviewInterface.GetReviews()
                .Where(r => r.Title == review.Title)
                .FirstOrDefault();

            if(testReview != null)
            {
                ModelState.AddModelError("", "Já existe uma review com esse titulo");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mappedReview = _mapper.Map<Review>(review);
            mappedReview.Pokemon = _pokemonInterface.GetPokemonById(pokeId);
            mappedReview.Reviewer = _reviewerInterface.GetReviewerById(reviewerId);
                

            if (!_reviewInterface.CreateReview(mappedReview))
            {
                ModelState.AddModelError("", "Houve um erro durante a operação");
                return StatusCode(500, ModelState);
            }

            return Ok("Review Criada com sucesso");
        }
    }
}
