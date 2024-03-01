using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_MedAppoint.Model;

namespace PRN221_Project_MedAppoint.Areas.Admin.Pages.ManageService
{
    public class IndexModel : PageModel
    {
        private readonly PRN221_Project_MedAppoint.Model.MyMedDbContext _context;

        public IndexModel(PRN221_Project_MedAppoint.Model.MyMedDbContext context)
        {
            _context = context;
        }

        public IList<Services> Services { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.Get("user") != null)
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
                ViewData["user"] = u;
                if (u.RoleID == 1)
                {
                    Services = await _context.Services.ToListAsync();
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
