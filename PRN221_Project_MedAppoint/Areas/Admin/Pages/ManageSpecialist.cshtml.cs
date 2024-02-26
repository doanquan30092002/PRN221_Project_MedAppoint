using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project_MedAppoint.Model;
using System.Text;
using System.Text.Json;

namespace PRN221_Project_MedAppoint.Areas.Admin.Pages
{
    public class ManageSpecialistModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (HttpContext.Session.Get("user") != null)
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
                ViewData["user"] = u;
                if (u.RoleID == 1)
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
                return RedirectToPage("/Index", new { area = "Admin" });
            }
        }
    }
}
