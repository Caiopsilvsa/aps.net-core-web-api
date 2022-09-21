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

        public bool CreateCountry(Country country)
        {
            _context.Add(country);
            return SaveCountry();
        }

        public bool SaveCountry()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
            _context.Update(country);
            return SaveCountry();
        }

        public bool DeleteCountry(Country country)
        {
            _context.Remove(country);
            return SaveCountry();
        }
    }
}
