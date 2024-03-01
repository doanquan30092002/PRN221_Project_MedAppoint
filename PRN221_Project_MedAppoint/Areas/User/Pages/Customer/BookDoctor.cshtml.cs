using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_MedAppoint.Model;
using System.Text;
using System.Text.Json;

namespace PRN221_Project_MedAppoint.Areas.User.Pages.Customer
{
    public class BookDoctorModel : PageModel
    {
        private readonly MyMedDbContext _context;

        public BookDoctorModel(MyMedDbContext context)
        {
            _context = context;
        }

        public IList<UserWithSpecialtiesViewModel> Users { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.Get("user") != null)
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
                ViewData["user"] = u;
                if (u.RoleID == 2)
                {
                    Users = _context.Users
                                        .Include(u => u.Role)
                                        .Include(u => u.UsersToSpecialists)
                                            .ThenInclude(us => us.Specialist)
                                        .Where(u => u.RoleID == 3)
                                        .Select(u => new UserWithSpecialtiesViewModel
                                        {
                                            User = u,
                                            Specialties = u.UsersToSpecialists.Select(us => us.Specialist.SpecialtyName).ToList()
                                        })
                                        .ToList();

                    return Page();
                }
                else
                {
                    return RedirectToPage("/Error", new { area = "User" });
                }
            }
            else
            {
                return RedirectToPage("/Login", new { area = "User" });
            }
        }

        public class UserWithSpecialtiesViewModel
        {
            public Users User { get; set; }
            public List<string> Specialties { get; set; }
        }

    }
}
