using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_MedAppoint.Helpers;
using PRN221_Project_MedAppoint.Model;
using PRN221_Project_MedAppoint.Validations;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace PRN221_Project_MedAppoint.Areas.User.Pages.Customer
{
    public class CheckoutModel : PageModel
    {

        private readonly MyMedDbContext _context;

        public CheckoutModel(MyMedDbContext context)
        {
            _context = context;
        }

        // Appointment input[
        [BindProperty]
        public AppointmentInput Input { get; set; } 

        public class AppointmentInput
        {
            [Required]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; }

            [Required]
            [DataType(DataType.Time)]
            [BookingTimeValidation]
            public DateTime BeginTime { get; set; }

            [Required]
            [DataType(DataType.Time)]
            [BookingTimeValidation]
            public DateTime EndTime { get; set; }
        }
        // End Appointment input

        [BindProperty]
        public int SpecialistID { get; set; }

        public List<SelectListItem> Specialists { get; set; }

        public UserWithSpecialtiesViewModel Doctor { get; set; }

        public IActionResult OnGet(int doctorID)
        {
            if (HttpContext.Session.Get("user") != null)
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
                ViewData["user"] = u;
                if (u.RoleID == 2)
                {
                    // Retrieve the user information and specialties
                    var doctor = _context.Users
                                    .Include(u => u.UsersToSpecialists)
                                    .ThenInclude(us => us.Specialist)
                                    .FirstOrDefault(u => u.UserID == doctorID);

                    if (doctor != null)
                    {
                        Doctor = new UserWithSpecialtiesViewModel
                        {
                            User = doctor,
                            Specialties = doctor.UsersToSpecialists.Select(us => us.Specialist.SpecialtyName).ToList()
                        };

                        Specialists = doctor.UsersToSpecialists.Select(s => new SelectListItem
                        {
                            Value = s.SpecialistID.ToString(),
                            Text = s.Specialist.SpecialtyName
                        }).ToList();
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
    }
}
