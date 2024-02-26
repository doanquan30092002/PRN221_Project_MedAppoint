using System.ComponentModel.DataAnnotations;

namespace PRN221_Project_MedAppoint.Model
{
    public class Roles
    {
        [Key]
        public int RoleID { get; set; }
        public string? RoleName { get; set; }
    }
}
