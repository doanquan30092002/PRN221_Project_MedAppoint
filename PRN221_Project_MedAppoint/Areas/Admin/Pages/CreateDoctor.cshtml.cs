using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using PRN221_Project_MedAppoint.Model;
using PRN221_Project_MedAppoint.Service;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace PRN221_Project_MedAppoint.Areas.Admin.Pages
{
    public class CreateDoctorModel : PageModel
    {
        private readonly MyMedDbContext _context;
        public CreateDoctorModel(MyMedDbContext myMedDbContext)
        {
            _context = myMedDbContext;
        }
        [BindProperty]
        public Users Doctor {  get; set; }
        public List<Specialist> Specialists { get; set; }
        [BindProperty]
        public List<string> SelectedOptions { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.Get("user") != null)
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
                ViewData["user"] = u;
                if (u.RoleID==1)
                {
                    Specialists=_context.Specialists.ToList();
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
        public IActionResult OnPost()
        {
            if (_context.Users.Where(x => x.Username.Equals(Doctor.Username)).FirstOrDefault() == null)
            {
                Users createDoctor = new Users()
                {
                    UserID = 0,
                    Username = Doctor.Username,
                    Password = AES.Encrypt(Doctor.Password),
                    Email = Doctor.Email,
                    Phone = Doctor.Phone,
                    Address = Doctor.Address,
                    RoleID = 3,
                    Avatar = null,
                    Gender = Doctor.Gender,
                    DoctorPrice = Doctor.DoctorPrice,
                    IsDeleted = false,
                };
                _context.Users.Add(createDoctor);
                _context.SaveChanges();
                // insert UsersToSpecialist
                Users findUserNew = _context.Users.FirstOrDefault(x => x.Username == Doctor.Username);
                List<UsersToSpecialist> specialists = new List<UsersToSpecialist>();
                foreach (var item in SelectedOptions)
                {
                    UsersToSpecialist usersToSpecialist = new UsersToSpecialist()
                    {
                        UserID = findUserNew.UserID,
                        SpecialistID = int.Parse(item),
                        IsDeleted = false,
                    };
                    specialists.Add(usersToSpecialist);
                }
                _context.UsersToSpecialists.AddRange(specialists);
                _context.SaveChanges();
                return RedirectToPage("/ManageDoctor", new { area = "Admin" });
            }
            else
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
                ViewData["user"] = u;
                ViewData["mess"] = "Username has exist!";
                return Page();
            }

        }

    }
}
