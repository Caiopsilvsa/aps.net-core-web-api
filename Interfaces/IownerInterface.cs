using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IOwnerInterface
    {
        ICollection<Owner> GetOwners();
        Owner GetOwnerById(int id);

        ICollection<Owner> GetOwnerByPokemon(int pokemonId);

        ICollection<Pokemon> GetPokemonByOwner(int ownerId);

        bool OwnerExist(int ownerId);
    }
}
