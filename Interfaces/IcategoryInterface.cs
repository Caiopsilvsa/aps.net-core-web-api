using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICategoryInterface
    {
        ICollection<Category> GetCategories();
        Category GetCategoryById(int id);
        ICollection<Pokemon> GetPokemonByCategory(int id);

        bool CategoryExist(int id);
    }
}
