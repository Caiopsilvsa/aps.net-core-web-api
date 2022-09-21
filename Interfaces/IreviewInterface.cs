using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewInterface
    {
        ICollection<Review> GetReviews();
        Review GetReviewById(int reviewId);
        ICollection<Review> GetReviewsByAPokemonId(int pokeId);
        bool CreateReview(Review review);
        bool SaveReview();
        bool UpdateReview(Review review);
        bool ReviewExist(int reviewId);
    }
}
