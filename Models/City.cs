using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgyptCitiesApi.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        [Column("governorate_id")]
        public int GovernorateId { get; set; }

        [StringLength(200)]
        [Column("city_name_ar", TypeName = "nvarchar(200)")]
        public string CityNameAr { get; set; } = default!;

        [StringLength(200)]
        [Column("city_name_en", TypeName = "nvarchar(200)")]
        public string CityNameEn { get; set; } = default!;

        // Navigation property for Governorate (Foreign Key)
        [ForeignKey("GovernorateId")]
        public virtual Governorate Governorate { get; set; } = default!;
    }
}