using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

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

        [HttpPost]
        [ProducesResponseType(204, Type = typeof(IEnumerable<Reviewer>))]
        [ProducesResponseType(404)]
        public IActionResult CreateReviewer([FromBody]ReviewerDto reviewer)
        {
            if(reviewer == null)
            {
                return BadRequest(ModelState);
            }

            var testReviewer = _reviewer.GetReviewers()
                .Where(r => r.FirstName.Trim().ToUpper() == reviewer.FirstName.Trim().ToUpper())
                .FirstOrDefault();

            if(testReviewer != null)
            {
                ModelState.AddModelError("", "Esse reviewer ja existe");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return NotFound(ModelState);
            }

           var mappedReviewer = _mapper.Map<Reviewer>(reviewer);

            if (!_reviewer.CreateReviewer(mappedReviewer))
            {
                ModelState.AddModelError("", "Houve um erro durante a operação");
                return StatusCode(500, ModelState);
            } 

            return Ok("Reviewer criado com sucesso!");

        }

        [HttpPut("{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReviewer(int reviewerId, [FromBody] ReviewerDto reviewer)
        {
            if (reviewer == null)
            {
                return BadRequest(ModelState);
            }

            if (reviewerId != reviewer.Id)
            {
                return BadRequest();
            }

            if (!_reviewer.ReviewerExist(reviewerId))
            {
                return NotFound(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return NotFound(ModelState);
            }

            var mappedReviewer = _mapper.Map<Reviewer>(reviewer);

            if (!_reviewer.UpdateReviewer(mappedReviewer))
            {
                ModelState.AddModelError("", "Houve um erro durante a operação");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
