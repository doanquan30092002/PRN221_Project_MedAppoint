using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project_MedAppoint.Model;
using PRN221_Project_MedAppoint.Service;
using System.Text;
using System.Text.Json;

namespace PRN221_Project_MedAppoint.Areas.Admin.Pages
{
    public class ManageCustomerModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (HttpContext.Session.Get("user") != null)
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
                ViewData["user"] = u;
                return Page();
            }
            else
            {
                return RedirectToPage("/Index", new { area = "Admin" });
            }
        }
    }
}
