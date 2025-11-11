using EgyptCitiesApi.Models;

namespace EgyptCitiesApi.Interfaces
{
    public interface ICityRepository : IGenericRepository<City>
    {
        Task<IEnumerable<City>> GetCitiesWithGovernoratesAsync();
        Task<City> GetCityWithGovernorateByIdAsync(int id);
    }
}
