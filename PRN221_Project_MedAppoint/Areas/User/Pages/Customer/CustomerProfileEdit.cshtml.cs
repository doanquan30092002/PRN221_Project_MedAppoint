using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_MedAppoint.Model;
using System.Text;
using System.Text.Json;

namespace PRN221_Project_MedAppoint.Areas.User.Pages.Customer
{
    public class CustomerProfileEditModel : PageModel
    {
        private readonly MyMedDbContext _context;

        public CustomerProfileEditModel(MyMedDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Users Users { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.Get("user") != null)
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
                ViewData["user"] = u;
                if (u.RoleID == 2)
                {
                    var users = await _context.Users.FirstOrDefaultAsync(m => m.UserID == id);
                    if (users == null)
                    {
                        return NotFound();
                    }
                    Users = users;

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

        public async Task<IActionResult> OnPostAsync()
        {
            _context.Attach(Users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) // check if other have change the same data in DB
            {
                if (!UsersExists(Users.UserID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./CustomerProfile");
        }

        private bool UsersExists(int id)
        {
            return (_context.Users?.Any(e => e.UserID == id)).GetValueOrDefault();
        }
    }
}
