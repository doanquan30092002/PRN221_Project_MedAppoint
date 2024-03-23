using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_MedAppoint.Model;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace PRN221_Project_MedAppoint.Areas.User.Pages.Customer
{
    public class CustomerProfileEditModel : PageModel
    {
        private readonly MyMedDbContext _context;
		private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

		public CustomerProfileEditModel(MyMedDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Users Users { get; set; } = default!;

		// upload file
		[BindProperty]
		[Required(ErrorMessage = "Please select a file.")]
		public IFormFile Upload { get; set; }

        public string rootPath { get; set; }
		// end upload file 


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


                    if (Users.Avatar != null)
                    {
                        rootPath = Path.Combine("./", Users.Avatar);
                    }


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

        public async Task<IActionResult> OnPostUserProfile()
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

		public async Task OnPostUploadFile()
		{
			byte[] userBytes = HttpContext.Session.Get("user");
			string serializedUser = Encoding.UTF8.GetString(userBytes);
			Users u = JsonSerializer.Deserialize<Users>(serializedUser);
			ViewData["user"] = u;
          
			var file = Path.Combine(_environment.ContentRootPath, "wwwroot", "user", "img", Upload.FileName);
			using (var fileStream = new FileStream(file, FileMode.Create))
			{
				await Upload.CopyToAsync(fileStream);
			}

			Users userAvatar = _context.Users.FirstOrDefault(u => u.UserID == Users.UserID);
			userAvatar.Avatar = Path.Combine("user", "img", Upload.FileName).Replace("\\", "/");
			Users = userAvatar;
			rootPath = Path.Combine("./", Users.Avatar);
			_context.SaveChanges();
		}
    }
}
