using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN221_Project_MedAppoint.Model
{
    public class Appointments
    {
        [Key]
        public int AppointmentID { get; set; }
        public int? UserID { get; set; }
        public int? DoctorID { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? EndDate { get; set; }
        public int? SpecialistID { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string? Status { get; set; }
        public Users User { get; set; }
        public Users Doctor { get; set; }
        public Specialist Specialist { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
