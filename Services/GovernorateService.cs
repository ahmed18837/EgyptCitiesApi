using AutoMapper;
using EgyptCitiesApi.DTOs;
using EgyptCitiesApi.Interfaces;
using EgyptCitiesApi.Models;

namespace EgyptCitiesApi.Services
{
    public class GovernorateService(IGenericRepository<Governorate> governorateRepository, IMapper mapper) : IGovernorateService
    {
        private readonly IGenericRepository<Governorate> _governorateRepository = governorateRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<GovernorateDto>> GetAllGovernoratesAsync()
        {
            var governorates = await _governorateRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GovernorateDto>>(governorates);
        }

        public async Task<GovernorateDto> GetGovernorateByIdAsync(int id)
        {
            var governorate = await _governorateRepository.GetByIdAsync(id);
            return _mapper.Map<GovernorateDto>(governorate);
        }

        public async Task<GovernorateDto> AddGovernorateAsync(GovernorateDto governorateDto)
        {
            var governorate = _mapper.Map<Governorate>(governorateDto);
            await _governorateRepository.AddAsync(governorate);
            await _governorateRepository.SaveChangesAsync();
            return _mapper.Map<GovernorateDto>(governorate);
        }

        public async Task<bool> UpdateGovernorateAsync(int id, GovernorateDto governorateDto)
        {
            var existingGovernorate = await _governorateRepository.GetByIdAsync(id);
            if (existingGovernorate == null)
            {
                return false;
            }

            // Map DTO data to existing entity
            _mapper.Map(governorateDto, existingGovernorate);
            _governorateRepository.Update(existingGovernorate);
            return await _governorateRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteGovernorateAsync(int id)
        {
            var existingGovernorate = await _governorateRepository.GetByIdAsync(id);
            if (existingGovernorate == null)
            {
                return false;
            }

            _governorateRepository.Delete(existingGovernorate);
            return await _governorateRepository.SaveChangesAsync();
        }
    }
}