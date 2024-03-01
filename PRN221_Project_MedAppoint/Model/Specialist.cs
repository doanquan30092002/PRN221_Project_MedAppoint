using System.ComponentModel.DataAnnotations;

namespace PRN221_Project_MedAppoint.Model
{
    public class Specialist
    {
        [Key]
        public int SpecialistID { get; set; }
        public string? SpecialtyName { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
