using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_MedAppoint.Model;
using System.Net;
using System.Text;
using System.Text.Json;

namespace PRN221_Project_MedAppoint.Areas.Admin.Pages
{
    public class UpdateDoctorModel : PageModel
    {
        private readonly MyMedDbContext _context;
        public UpdateDoctorModel(MyMedDbContext myMedDbContext)
        {
            _context = myMedDbContext;
        }
        [BindProperty]
        public Users Doctor { get; set; }
        public List<Specialist> Specialists { get; set; }
        [BindProperty]
        public List<string> SelectedOptions { get; set; }
        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.Get("user") != null)
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
                ViewData["user"] = u;
                if (u.RoleID == 1)
                {
                    ViewData["id"] = id;
                    Specialists = _context.Specialists.ToList();
                    Doctor = _context.Users.FirstOrDefault(x => x.UserID == id);
                    SelectedOptions = _context.UsersToSpecialists.Where(x => x.UserID == id).Select(x => x.SpecialistID.ToString()).ToList();
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
        public IActionResult OnPost(int id)
        {
            Users users = _context.Users.FirstOrDefault(x=>x.UserID == id);
            users.Email = Doctor.Email;
            users.Phone = Doctor.Phone;
            users.Address = Doctor.Address;
            users.Gender = Doctor.Gender;
            users.DoctorPrice = Doctor.DoctorPrice;
            _context.Attach(users).State = EntityState.Modified;
            _context.SaveChanges();
            //remove UsersToSpecialist by userID
            List<UsersToSpecialist> specialistsRemove = _context.UsersToSpecialists.Where(x => x.UserID == id).ToList();
            _context.RemoveRange(specialistsRemove);
            _context.SaveChanges();
            // insert UsersToSpecialist
            List<UsersToSpecialist> specialists = new List<UsersToSpecialist>();
            foreach (var item in SelectedOptions)
            {
                UsersToSpecialist usersToSpecialist = new UsersToSpecialist()
                {
                    UserID = users.UserID,
                    SpecialistID = int.Parse(item),
                    IsDeleted = false,
                };
                specialists.Add(usersToSpecialist);
            }
            _context.UsersToSpecialists.AddRange(specialists);
            _context.SaveChanges();

            return RedirectToPage("/ManageDoctor", new { area = "Admin" });
        }
       
    }
}
