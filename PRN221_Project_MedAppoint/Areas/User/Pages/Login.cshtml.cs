using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project_MedAppoint.Model;
using PRN221_Project_MedAppoint.Service;
using System.Text;
using System.Text.Json;

namespace PRN221_Project_MedAppoint.Areas.User.Pages
{
    public class LoginModel : PageModel
    {
        private readonly MyMedDbContext _myMedDbContext;
        public LoginModel(MyMedDbContext myMedDbContext)
        {
            _myMedDbContext = myMedDbContext;
        }

        [BindProperty]
        public Users Customer { get; set; }

        public void OnGet()
        {
            HttpContext.Session.Remove("user");
        }
        public IActionResult OnPost()
        {

            Users u = (from user in _myMedDbContext.Users
                       where user.Username.Equals(Customer.Username)
                       where user.Password.Equals(AES.Encrypt(Customer.Password))
                       select user).FirstOrDefault();

            if (u != null && u.RoleID == 2)
            {
                string serializedUser = JsonSerializer.Serialize(u);
                byte[] userBytes = Encoding.UTF8.GetBytes(serializedUser);
                HttpContext.Session.Set("user", userBytes);
                ViewData["user"] = u;
                return RedirectToPage("/Customer/Home", new { area = "User" });
            }
            else if (u != null && u.RoleID == 3)
            {
                string serializedUser = JsonSerializer.Serialize(u);
                byte[] userBytes = Encoding.UTF8.GetBytes(serializedUser);
                HttpContext.Session.Set("user", userBytes);
                return RedirectToPage("/Doctor/Doctors", new { area = "User" });
            }
            else
            {
                return Page();
            }


        }
    }
}
