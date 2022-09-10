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
    }
}
