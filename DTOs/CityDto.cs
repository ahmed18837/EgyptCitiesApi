namespace EgyptCitiesApi.DTOs
{
    public class CityDto
    {
        public int Id { get; set; }
        public string CityNameAr { get; set; } = default!;
        public string CityNameEn { get; set; } = default!;
        public int GovernorateId { get; set; }
        public string GovernorateNameEn { get; set; } = default!; 
        public string GovernorateNameAr { get; set; } = default!; 
    }

}
