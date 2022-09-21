using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewRepository : IReviewInterface
    {
        private readonly DataContext _context;
        public ReviewRepository(DataContext dataContext)
        {
            this._context = dataContext;
        }

        public bool CreateReview(Review review)
        {
            _context.Add(review);
            return SaveReview();
        }

        public Review GetReviewById(int reviewId)
        {
            return _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public ICollection<Review> GetReviewsByAPokemonId(int pokeId)
        {
            return _context.Reviews.Where(r => r.Pokemon.Id == pokeId).ToList();
        }

        public bool ReviewExist(int reviewId)
        {
            return _context.Reviews.Any(r => r.Id == reviewId);
        }

        public bool SaveReview()
        {
            var savedReview = _context.SaveChanges();

            return savedReview > 0 ? true : false;

        }

        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            return SaveReview();
        }
    }
}
