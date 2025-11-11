using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgyptCitiesApi.Models
{
    public class Governorate
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Column("governorate_name_ar", TypeName = "nvarchar(50)")]
        public string GovernorateNameAr { get; set; } = default!;

        [StringLength(50)]
        [Column("governorate_name_en", TypeName = "nvarchar(50)")]
        public string GovernorateNameEn { get; set; } = default!;

        // Navigation property for Cities
        public virtual ICollection<City> Cities { get; set; } = new List<City>();
    }
}
