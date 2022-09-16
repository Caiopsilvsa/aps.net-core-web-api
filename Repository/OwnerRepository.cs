using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class OwnerRepository : IOwnerInterface
    {
        private readonly DataContext _context;
        public OwnerRepository(DataContext dataContext)
        {
            this._context = dataContext;
        }
        public Owner GetOwnerById(int id)
        {
            return _context.Owners.Where(data => data.Id == id).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerByPokemon(int pokemonId)
        {
            return _context.PokemonOwners.Where(data => data.PokemonId == pokemonId).Select(o => o.Owner).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
        {
            return _context.PokemonOwners.Where(data => data.OwnerId == ownerId).Select(p => p.Pokemon).ToList();
        }

        public bool OwnerExist(int ownerId)
        {
            return _context.Owners.Any(data => data.Id == ownerId);
        }
    }
}
