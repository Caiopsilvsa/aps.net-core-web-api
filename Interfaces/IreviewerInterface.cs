using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewerInterface
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewerById(int reviewerId);
        ICollection<Review> GetReviewsByReviewerId(int reviewerId);
        bool ReviewerExist(int reviewerId);
    }
}
