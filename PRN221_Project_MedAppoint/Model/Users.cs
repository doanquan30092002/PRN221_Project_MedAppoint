using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace PRN221_Project_MedAppoint.Model
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(255)]
        public string? Username { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(255,MinimumLength =6)]
        public string? Password { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? Phone { get; set; }
        public int? SpecialtyID { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(255)]
        public string? Address { get; set; }
        public int? RoleID { get; set; }
        [Column(TypeName = "VARCHAR")]
        public string? Avatar { get; set; }
        public bool? Gender { get; set; }

        public Roles Role { get; set; }
    }
}
