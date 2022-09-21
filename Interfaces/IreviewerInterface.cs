using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewerInterface
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewerById(int reviewerId);
        ICollection<Review> GetReviewsByReviewerId(int reviewerId);
        bool CreateReviewer (Reviewer reviewer);
        bool SaveReviewer();
        bool UpdateReviewer(Reviewer reviewer);
        bool ReviewerExist(int reviewerId);
    }
}
