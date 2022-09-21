using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Controllers;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewerRepository : IReviewerInterface
    {
        private readonly DataContext _dataContext;
     
        public ReviewerRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _dataContext.Add(reviewer);
            return SaveReviewer();
        }

        public Reviewer GetReviewerById(int reviewerId)
        {
            return _dataContext.Reviewers.Where(r => r.Id == reviewerId).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _dataContext.Reviewers.ToList();
        }

        public ICollection<Review> GetReviewsByReviewerId(int reviewerId)
        {
            return _dataContext.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public bool ReviewerExist(int reviewerId)
        {
            return _dataContext.Reviewers.Any(r => r.Id == reviewerId);
        }

        public bool SaveReviewer()
        {
            var savedReviewer = _dataContext.SaveChanges();
            return savedReviewer > 0 ? true : false;
        }
    }
}
