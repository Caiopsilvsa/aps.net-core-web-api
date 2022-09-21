using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IPokemonInterface
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemonById(int id);
        Pokemon GetPokemonByName(string name);
        bool PokemonExists(int id);
        bool CreatePokemon(Pokemon pokemon, int categoryId, int ownerId);
        bool UpdatePokemon(Pokemon pokemon, int categoryId, int ownerId);
        bool DeletePokemon(Pokemon pokemon);
        bool SavePokemon();
    }
}
