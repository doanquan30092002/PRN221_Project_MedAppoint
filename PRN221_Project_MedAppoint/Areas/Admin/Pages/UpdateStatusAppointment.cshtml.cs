using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_MedAppoint.Model;
using System.Text;
using System.Text.Json;

namespace PRN221_Project_MedAppoint.Areas.Admin.Pages
{
    public class UpdateStatusAppointmentModel : PageModel
    {
        private readonly MyMedDbContext _context;

        public UpdateStatusAppointmentModel(MyMedDbContext context)
        {
            _context = context;
        }
        public Appointments Appointment { get; set; }
        public IActionResult OnGet(int appointmentID, string FilterStatus)
        {
            if (HttpContext.Session.Get("user") != null)
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
                ViewData["user"] = u;
                if (u.RoleID == 1)
                {
                    ViewData["appointmentID"] = appointmentID;
                    ViewData["FilterStatus"] = FilterStatus;
                    Appointment = _context.Appointments.Include(x=>x.User).Include(x=>x.Doctor).Include(x=>x.Specialist).FirstOrDefault(x=>x.AppointmentID== appointmentID);
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
        public IActionResult OnPost(int appointmentID, string valueSelectStatus,string FilterStatus)
        {
            Appointments appointment = _context.Appointments.FirstOrDefault(x=>x.AppointmentID== appointmentID);
            appointment.Status = valueSelectStatus;
            _context.Attach(appointment).State= EntityState.Modified;
            _context.SaveChanges();
            return RedirectToPage("/ManageAppointment", new { area = "Admin" , FilterStatus = FilterStatus });
        }
    }
}
