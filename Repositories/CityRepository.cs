using EgyptCitiesApi.Data;
using EgyptCitiesApi.Interfaces;
using EgyptCitiesApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EgyptCitiesApi.Repositories
{
    public class CityRepository(ApplicationDbContext context) : GenericRepository<City>(context), ICityRepository
    {
        public override async Task<IEnumerable<City>> FindAsync(Expression<Func<City, bool>> predicate)
        {
            return await _dbSet
                .Include(c => c.Governorate)
                .Where(predicate)
                .ToListAsync();
        }
        public async Task<IEnumerable<City>> GetCitiesWithGovernoratesAsync()
        {
            return await _dbSet.Include(c => c.Governorate).ToListAsync();
        }

        public async Task<City> GetCityWithGovernorateByIdAsync(int id)
        {
            var city = await _dbSet.Include(c => c.Governorate).FirstOrDefaultAsync(c => c.Id == id);
            return city!;
        }
    }
}
