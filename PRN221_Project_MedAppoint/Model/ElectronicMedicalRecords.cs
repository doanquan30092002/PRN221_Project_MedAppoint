using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN221_Project_MedAppoint.Model
{
    public class ElectronicMedicalRecords
    {
        [Key]
        public int EMR_ID { get; set; }
        public int? AppointmentID { get; set; }
        [Column(TypeName = "NTEXT")]
        public string? TestResults { get; set; }
        [Column(TypeName = "NTEXT")]
        public string? TreatmentPlans { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? LastUpdated { get; set; }
        public Appointments Appointment { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
