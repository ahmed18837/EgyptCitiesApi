using AutoMapper;
using EgyptCitiesApi.DTOs;
using EgyptCitiesApi.Models;

namespace EgyptCitiesApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Governorate, GovernorateDto>().ReverseMap();

            CreateMap<City, CityDto>()
                .ForMember(dest => dest.GovernorateNameEn,
                            opt => opt.MapFrom(src => src.Governorate.GovernorateNameEn))
                .ForMember(dest => dest.GovernorateNameAr,
                            opt => opt.MapFrom(src => src.Governorate.GovernorateNameAr))
                .ForMember(dest => dest.GovernorateNameEn,
                            opt => opt.NullSubstitute(string.Empty));

            CreateMap<CityDto, City>(); 
        }
    }
}
