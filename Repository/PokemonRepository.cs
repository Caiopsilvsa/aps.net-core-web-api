using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonInterface
    {
        private readonly DataContext _dataContext;

        public PokemonRepository(DataContext context)
        {
            this._dataContext = context;
        }

        public bool CreatePokemon(Pokemon pokemon, int categoryId, int ownerId)
        {
            var getCategory = _dataContext.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
            var getOwner = _dataContext.Owners.Where(o => o.Id == ownerId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = getOwner,
                Pokemon = pokemon,
            };
            _dataContext.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = getCategory,
                Pokemon = pokemon,
            };
            _dataContext.Add(pokemonCategory);

            _dataContext.Add(pokemon);

            return SavePokemon();
        }

        public bool DeletePokemon(Pokemon pokemon)
        {
            _dataContext.Remove(pokemon);
            return SavePokemon();
        }

        public Pokemon GetPokemonById(int id)
        {
            return _dataContext.Pokemon.Where(data => data.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemonByName(string name)
        {
            return _dataContext.Pokemon.Where(data => data.Name == name).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _dataContext.Pokemon.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int id)
        {
            return _dataContext.Pokemon.Any(data => data.Id == id);

        }

        public bool SavePokemon()
        {
            var savedChanges = _dataContext.SaveChanges();

            return savedChanges > 0 ? true : false;
        }

        public bool UpdatePokemon(Pokemon pokemon, int categoryId, int ownerId)
        {
            _dataContext.Update(pokemon);
            return SavePokemon();
        }
    }
}
