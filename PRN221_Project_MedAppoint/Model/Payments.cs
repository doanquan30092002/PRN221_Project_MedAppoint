using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN221_Project_MedAppoint.Model
{
    public class Payments
    {
        [Key]
        public int PaymentID { get; set; }
        public int? UserID { get; set; }
        public int? AppointmentID { get; set; }
        public decimal? Amount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PaymentDate { get; set; }
        [Column(TypeName = "NTEXT")]
        public string? Message { get; set; }
        public Users User { get; set; }
        public Appointments Appointment { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
