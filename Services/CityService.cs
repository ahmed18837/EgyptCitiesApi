using AutoMapper;
using EgyptCitiesApi.DTOs;
using EgyptCitiesApi.Interfaces;
using EgyptCitiesApi.Models;

namespace EgyptCitiesApi.Services
{
    public class CityService(ICityRepository cityRepository, IGenericRepository<Governorate> governorateRepository, IMapper mapper) : ICityService
    {
        private readonly ICityRepository _cityRepository = cityRepository; // نستخدم Repository المحدد
        private readonly IGenericRepository<Governorate> _governorateRepository = governorateRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<CityDto>> GetAllCitiesAsync()
        {
            // نستخدم دالة GetCitiesWithGovernoratesAsync لتحميل بيانات المحافظة مع المدينة
            var cities = await _cityRepository.GetCitiesWithGovernoratesAsync();
            return _mapper.Map<IEnumerable<CityDto>>(cities);
        }

        public async Task<CityDto> GetCityByIdAsync(int id)
        {
            var city = await _cityRepository.GetCityWithGovernorateByIdAsync(id);
            return _mapper.Map<CityDto>(city);
        }

        public async Task<IEnumerable<CityDto>> GetCitiesByGovernorateIdAsync(int governorateId)
        {
            var citiesFiltered = await _cityRepository.FindAsync(c => c.GovernorateId == governorateId);

            return _mapper.Map<IEnumerable<CityDto>>(citiesFiltered);
        }

        public async Task<CityDto> AddCityAsync(CityDto cityDto)
        {
            // تحقق من وجود المحافظة قبل الإضافة
            var governorate = await _governorateRepository.GetByIdAsync(cityDto.GovernorateId);
            if (governorate == null)
            {
                // يمكن رمي استثناء أو التعامل مع الخطأ هنا
                return null;
            }

            var city = _mapper.Map<City>(cityDto);
            await _cityRepository.AddAsync(city);
            await _cityRepository.SaveChangesAsync();

            // للحصول على GovernorateNameEn في الـ DTO المرجع
            city.Governorate = governorate;
            return _mapper.Map<CityDto>(city);
        }

        public async Task<bool> UpdateCityAsync(int id, CityDto cityDto)
        {
            var existingCity = await _cityRepository.GetByIdAsync(id);
            if (existingCity == null)
            {
                return false;
            }

            // التأكد من أن الـ ID في الـ DTO يطابق الـ ID في المسار
            cityDto.Id = id;

            // Map DTO data to existing entity
            _mapper.Map(cityDto, existingCity);
            _cityRepository.Update(existingCity);
            return await _cityRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteCityAsync(int id)
        {
            var existingCity = await _cityRepository.GetByIdAsync(id);
            if (existingCity == null)
            {
                return false;
            }

            _cityRepository.Delete(existingCity);
            return await _cityRepository.SaveChangesAsync();
        }
    }

}
