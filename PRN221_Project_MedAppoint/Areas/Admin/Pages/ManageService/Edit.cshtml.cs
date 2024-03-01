using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_MedAppoint.Model;

namespace PRN221_Project_MedAppoint.Areas.Admin.Pages.ManageService
{
    public class EditModel : PageModel
    {
        private readonly PRN221_Project_MedAppoint.Model.MyMedDbContext _context;

        public EditModel(PRN221_Project_MedAppoint.Model.MyMedDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Services Services { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.Get("user") != null)
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
                ViewData["user"] = u;
                if (u.RoleID == 1)
                {
                    if (id == null || _context.Services == null)
                    {
                        return NotFound();
                    }

                    var services = await _context.Services.FirstOrDefaultAsync(m => m.ServiceID == id);
                    if (services == null)
                    {
                        return NotFound();
                    }
                    Services = services;
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Services).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicesExists(Services.ServiceID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ServicesExists(int id)
        {
          return (_context.Services?.Any(e => e.ServiceID == id)).GetValueOrDefault();
        }
    }
}
