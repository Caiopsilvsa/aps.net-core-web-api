using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CategoryRepository : ICategoryInterface
    {
        private readonly DataContext _datacontext;
        public CategoryRepository(DataContext dataContext)
        {
            this._datacontext = dataContext;
        }

        public ICollection<Category> GetCategories()
        {
            return _datacontext.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _datacontext.Categories.Where(data => data.Id == id).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonByCategory(int id)
        {
            return _datacontext.PokemonCategories.Where(e=> e.CategoryId == id).Select(c=>c.Pokemon).ToList();
        }

        public bool SaveCategory(Category category)
        {
            _datacontext.Add(category);
            return Save();
        }

        public bool Save()
        {
            var saved = _datacontext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool CategoryExist(int id)
        {
            return _datacontext.Categories.Any(data => data.Id == id);
        }

        public bool UpdateCategory(Category category)
        {
            _datacontext.Update(category);
            return Save();
        }
    }
}
