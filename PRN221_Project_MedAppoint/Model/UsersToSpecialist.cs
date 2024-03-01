using System.ComponentModel.DataAnnotations;

namespace PRN221_Project_MedAppoint.Model
{
    public class UsersToSpecialist
    {
        [Key]
        public int UsersToSpecialistID { get; set; }
        public int? UserID { get; set; }
        public int? SpecialistID { get; set; }
        public Users User { get; set; }
        public Specialist Specialist { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
