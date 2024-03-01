using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PRN221_Project_MedAppoint.Model
{
    public class Services
    {
        [Key]
        public int ServiceID { get; set; }

        [Required]
        [MaxLength(100)]
        public string? ServiceName { get; set; }

        [Required]
        public decimal? Price { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
