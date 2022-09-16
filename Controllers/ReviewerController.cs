using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewerController:Controller
    {
        private readonly IReviewerInterface _reviewer;
        private readonly IMapper _mapper;
        public ReviewerController(IReviewerInterface reviewer, IMapper mapper)
        {
            this._reviewer = reviewer;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        public IActionResult GetAllReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerDto>>(_reviewer.GetReviewers());

            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            return Ok(reviewers);
        }

        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewerById(int reviewerId)
        {
            if (!_reviewer.ReviewerExist(reviewerId))
            {
                return BadRequest();
            }

            var reviewer = _mapper.Map<ReviewerDto>(_reviewer.GetReviewerById(reviewerId));

            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            return Ok(reviewer);
        }

        [HttpGet("/review/{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewByReviewerId(int reviewerId)
        {
            if (!_reviewer.ReviewerExist(reviewerId))
            {
                return BadRequest();
            }

            var reviewers = _mapper.Map<List<ReviewDto>>(_reviewer.GetReviewsByReviewerId(reviewerId));

            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            return Ok(reviewers);
        }


    }
}
