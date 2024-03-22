using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_MedAppoint.Model;
using System.Text;
using System.Text.Json;

namespace PRN221_Project_MedAppoint.Areas.User.Pages.Doctor
{
    public class TrackingMedicalHistoryModel : PageModel
    {
        private readonly MyMedDbContext _context;

        public TrackingMedicalHistoryModel(MyMedDbContext context)
        {
            _context = context;
        }

        public Users Patient { get; set; }
        public List<ElectronicMedicalRecords> ElectronicMedicalRecords { get; set; }
        public IActionResult OnGet(int patientInformationId)
        {
            if (HttpContext.Session.Get("user") != null)
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
                ViewData["user"] = u;
                if (u.RoleID == 3)
                {
                    Patient = _context.Users.FirstOrDefault(x=>x.UserID==patientInformationId);
                    ElectronicMedicalRecords = _context.ElectronicMedicalRecords.Include(x=>x.Appointment).ThenInclude(x=>x.Doctor).Where(x=>x.Appointment.UserID== patientInformationId).ToList();
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
