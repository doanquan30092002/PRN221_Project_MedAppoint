using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_MedAppoint.Model;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace PRN221_Project_MedAppoint.Areas.User.Pages.Doctor
{
    public class DoctorProfileEditModel : PageModel
    {
		private readonly MyMedDbContext _context;

		public DoctorProfileEditModel(MyMedDbContext context)
		{
			_context = context;
		}
		[BindProperty]
		public Users Doctor { get; set; }
		
		public IActionResult OnGet()
        {
            if (HttpContext.Session.Get("user") != null)
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
				ViewData["user"] = _context.Users.FirstOrDefault(x => x.UserID == u.UserID);
				if (u.RoleID == 3)
                {
                    Doctor = _context.Users.FirstOrDefault(x => x.UserID == u.UserID);

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
        public IActionResult Onpost()
        {
			Users userUpdate = _context.Users.FirstOrDefault(x=>x.UserID == Doctor.UserID);
            if (userUpdate != null)
            {
                userUpdate.Address = Doctor.Address;
                userUpdate.Phone = Doctor.Phone;
                userUpdate.Email = Doctor.Email;

			}
				_context.Attach(userUpdate).State = EntityState.Modified;
                _context.SaveChanges();
				return RedirectToPage("/Doctor/DoctorProfile", new { area = "User" });
            
            
        }
    }
}
