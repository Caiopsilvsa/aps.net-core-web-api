using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICountryInterface
    {
        ICollection<Country> GetContries();
        Country GetCountryById(int id);
        Country GetContryByName(string name);

        ICollection<Country> GetCountryByOwner(int OwnerId);

        bool ContryExist(int id);

    }
}
