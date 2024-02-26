using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project_MedAppoint.Model;
using System.Text.Json;
using System.Text;

namespace PRN221_Project_MedAppoint.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MyMedDbContext _myMedDbContext;
        public IndexModel(MyMedDbContext myMedDbContext)
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
                       where user.Password.Equals(Customer.Password)
                       select user).FirstOrDefault();

            if (u != null && u.RoleID == 1)
            {
                string serializedUser = JsonSerializer.Serialize(u);
                byte[] userBytes = Encoding.UTF8.GetBytes(serializedUser);
                HttpContext.Session.Set("user", userBytes);
                return RedirectToPage("/ManageCustomer", new { area = "Admin" });
            }
            else
            {
                return Page();
            }


        }
    }
}
