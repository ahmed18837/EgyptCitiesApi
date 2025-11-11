using EgyptCitiesApi.DTOs;

namespace EgyptCitiesApi.Interfaces
{
    public interface ICityService
    {
        // CRUD Operations
        Task<IEnumerable<CityDto>> GetAllCitiesAsync();
        Task<CityDto> GetCityByIdAsync(int id);
        Task<CityDto> AddCityAsync(CityDto cityDto);
        Task<bool> UpdateCityAsync(int id, CityDto cityDto);
        Task<bool> DeleteCityAsync(int id);

        // Specific Query
        Task<IEnumerable<CityDto>> GetCitiesByGovernorateIdAsync(int governorateId);
    }

}
