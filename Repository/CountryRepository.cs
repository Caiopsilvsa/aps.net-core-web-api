using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CountryRepository : ICountryInterface
    {
        private readonly DataContext _context;
        public CountryRepository(DataContext dataContext)
        {
            this._context = dataContext;
        }
        public ICollection<Country> GetContries()
        {
            return _context.Countries.OrderBy(data => data.Id).ToList();
        }

        public Country GetContryByName(string name)
        {
            return _context.Countries.Where(data => data.Name == name).FirstOrDefault();
        }

        public Country GetCountryById(int id)
        {
            return _context.Countries.Where(data => data.Id == id).FirstOrDefault();
        }

        public ICollection<Country> GetCountryByOwner(int OwnerId)
        {
            return _context.Owners.Where(data => data.Id == OwnerId).Select(c => c.Country).ToList();
        }

        public bool ContryExist(int id)
        {
            return _context.Countries.Any(data => data.Id == id);
        }
    }
}
