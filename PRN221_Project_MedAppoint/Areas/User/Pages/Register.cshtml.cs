using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project_MedAppoint.Model;
using PRN221_Project_MedAppoint.Service;

namespace PRN221_Project_MedAppoint.Areas.User.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly MyMedDbContext _myMedDbContext;
        public RegisterModel(MyMedDbContext myMedDbContext)
        {
            _myMedDbContext = myMedDbContext;
        }

        [BindProperty]
        public Users Customer { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {

            Users u = (from user in _myMedDbContext.Users
                       where user.Username.Equals(Customer.Username)
                       select user).FirstOrDefault();

            if (u != null)
            {
                ViewData["mess"] = "Username has exits";
                return Page();
            }

            else
            {
                if (Customer.Password != Request.Form["ConfirmPassword"])
                {
                    ViewData["mess"] = "khac pass word";
                    return Page();
                }
                else
                {
                    Users userRegister = new Users()
                    {
                        Username = Customer.Username,
                        Password = AES.Encrypt(Customer.Password),
                        RoleID = 2
                    };
                    _myMedDbContext.Users.Add(userRegister);
                    _myMedDbContext.SaveChanges();
                    return RedirectToPage("/Login");
                }
            }


        }
    }
}
