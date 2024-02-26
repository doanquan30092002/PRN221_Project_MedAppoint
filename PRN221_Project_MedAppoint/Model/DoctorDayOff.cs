using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN221_Project_MedAppoint.Model
{
    public class DoctorDayOff
    {
        [Key]
        public int DoctorDayOffID { get; set; }
        public int? DoctorID { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? EndDate { get; set; }
        public Users Doctor { get; set; }
    }
}
