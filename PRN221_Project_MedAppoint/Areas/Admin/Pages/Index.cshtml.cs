using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project_MedAppoint.Model;
using System.Text.Json;
using System.Text;
using System.ComponentModel.DataAnnotations;
using PRN221_Project_MedAppoint.Service;

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
		public InputModel Customer { get; set; }
		public class InputModel
		{

			[Required]
			//[EmailAddress]
			[Display(Name = "User name")]
			public string Username { get; set; }

			[Required]
			[StringLength(255, MinimumLength = 6)]
			public string Password { get; set; }
		}

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

            if (u != null && u.RoleID == 1)
            {
                string serializedUser = JsonSerializer.Serialize(u);
                byte[] userBytes = Encoding.UTF8.GetBytes(serializedUser);
                HttpContext.Session.Set("user", userBytes);
                return RedirectToPage("/ManageAppointment", new { area = "Admin" });
            }
            else
            {
                return Page();
            }


        }
    }
}
