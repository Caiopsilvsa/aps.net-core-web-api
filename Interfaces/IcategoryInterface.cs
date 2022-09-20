using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICategoryInterface
    {
        ICollection<Category> GetCategories();
        Category GetCategoryById(int id);
        ICollection<Pokemon> GetPokemonByCategory(int id);

        bool SaveCategory(Category category);
        bool Save();

        bool CategoryExist(int id);
    }
}
