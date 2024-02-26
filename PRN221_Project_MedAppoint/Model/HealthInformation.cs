using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN221_Project_MedAppoint.Model
{
    public class HealthInformation
    {
        [Key]
        public int HealthInfoID { get; set; }
        public int? UserID { get; set; }
        [Column(TypeName = "NTEXT")]
        public string? Allergies { get; set; }
        [Column(TypeName = "NTEXT")]
        public string? Medications { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public string? BloodType { get; set; }
        [Column(TypeName = "NTEXT")]
        public string? HealthHistory { get; set; }
        public Users User { get; set; }
    }
}
