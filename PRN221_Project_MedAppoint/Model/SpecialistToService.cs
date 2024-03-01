using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN221_Project_MedAppoint.Model
{
    public class SpecialistToService
    {
        [Key]
        public int SpecialistToServiceID { get; set; }

        public int SpecialistID { get; set; }

        public int ServiceID { get; set; }

        [ForeignKey("SpecialistID")]
        public Specialist Specialist { get; set; }

        [ForeignKey("ServiceID")]
        public Services Services { get; set; }
    }
}
