using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_MedAppoint.Helpers;
using PRN221_Project_MedAppoint.Model;
using PRN221_Project_MedAppoint.Service;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace PRN221_Project_MedAppoint.Areas.User.Pages.Customer
{
    public class CheckoutModel : PageModel
    {

        private readonly MyMedDbContext _context;
        private IMomoService _momoService;

        public CheckoutModel(MyMedDbContext context, IMomoService momoService)
        {
            _context = context;
            _momoService = momoService;
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
            public DateTime BeginTime { get; set; }
        }
        // End Appointment input

        [BindProperty]
        public int SpecialistID { get; set; }

        public List<SelectListItem> Specialists { get; set; }

        [BindProperty]
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

        public async Task<IActionResult> OnPostBookingAppointment()
        {
            byte[] userBytes = HttpContext.Session.Get("user");
            string serializedUser = Encoding.UTF8.GetString(userBytes);
            Users u = JsonSerializer.Deserialize<Users>(serializedUser);

            Appointments app = new Appointments
            {
                UserID = u.UserID,
                DoctorID = Doctor.User.UserID,
                StartDate = Input.Date.Date + Input.BeginTime.TimeOfDay,
                EndDate = Input.Date.Date + Input.BeginTime.TimeOfDay + TimeSpan.FromHours(1),
                SpecialistID = SpecialistID,
                Status = Status.Pending.ToString(),
            };

            _context.Appointments.Add(app);
            await _context.SaveChangesAsync();

            //Momo
            Payments order = new Payments
            {
                UserID = u.UserID,
                AppointmentID = app.AppointmentID, 
                Amount = Doctor.User.DoctorPrice,
                Message = "Khach hang: " + u.UserID + " da tra bang Momo" 
            };

            var response = await _momoService.CreatePaymentAsync(order);

            return Redirect(response.PayUrl);
        }
    }
}
