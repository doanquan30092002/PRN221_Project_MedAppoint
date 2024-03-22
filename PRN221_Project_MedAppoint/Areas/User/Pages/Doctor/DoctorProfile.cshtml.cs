using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project_MedAppoint.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json;

namespace PRN221_Project_MedAppoint.Areas.User.Pages.Doctor
{
    public class DoctorProfileModel : PageModel
    {
		private readonly MyMedDbContext _context;

		public DoctorProfileModel(MyMedDbContext context)
		{
			_context = context;
		}
		public IActionResult OnGet()
        {
            if (HttpContext.Session.Get("user") != null)
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
                ViewData["user"] = _context.Users.FirstOrDefault(x=>x.UserID == u.UserID);
                if (u.RoleID == 3)
                {
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
