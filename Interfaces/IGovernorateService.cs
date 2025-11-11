using EgyptCitiesApi.DTOs;

namespace EgyptCitiesApi.Interfaces
{
    public interface IGovernorateService
    {
        Task<IEnumerable<GovernorateDto>> GetAllGovernoratesAsync();
        Task<GovernorateDto> GetGovernorateByIdAsync(int id);
        Task<GovernorateDto> AddGovernorateAsync(GovernorateDto governorateDto);
        Task<bool> UpdateGovernorateAsync(int id, GovernorateDto governorateDto);
        Task<bool> DeleteGovernorateAsync(int id);
    }
}
