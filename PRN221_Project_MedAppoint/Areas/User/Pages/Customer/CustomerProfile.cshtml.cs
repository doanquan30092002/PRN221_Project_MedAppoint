using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project_MedAppoint.Helpers;
using PRN221_Project_MedAppoint.Model;
using System.Text;
using System.Text.Json;

namespace PRN221_Project_MedAppoint.Areas.User.Pages.Customer
{
    public class CustomerProfileModel : PageModel
    {
        private readonly MyMedDbContext _context;

        public CustomerProfileModel(MyMedDbContext context)
        {
            _context = context;
        }

        public UserWithSpecialtiesViewModel Users { get; set; }

        public IActionResult OnGetAsync()
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
                            .Where(us => us.UserID == u.UserID)
                            .Select(u => new UserWithSpecialtiesViewModel
                            {
                                User = u,
                                Specialties = u.UsersToSpecialists.Select(us => us.Specialist.SpecialtyName).ToList()
                            })
                            .FirstOrDefault();

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
    }
}
