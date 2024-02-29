using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN221_Project_MedAppoint.Model
{
    public class DoctorReviews
    {
        [Key]
        public int ReviewID { get; set; }
        public int? UserID { get; set; }
        public int? DoctorID { get; set; }
        [Column(TypeName = "NTEXT")]
        public string? Comment { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? ReviewDate { get; set; }
        public Users User { get; set; }
        public Users Doctor { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
