using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_MedAppoint.Model;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace PRN221_Project_MedAppoint.Areas.User.Pages.Doctor
{
    public class CreateElectronicMedicalModel : PageModel
    {
        private readonly MyMedDbContext _myMedDbContext;
        public CreateElectronicMedicalModel(MyMedDbContext myMedDbContext)
        {
            _myMedDbContext = myMedDbContext;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {

            [Required]
            public string TestResults { get; set; }

            [Required]
            public string TreatmentPlans { get; set; }

            public int? AppointmentID { get; set; }
            public int? PatientInformationId { get; set; }
        }
        public IActionResult OnGet(int appointmentID, int patientInformationId)
        {
            if (HttpContext.Session.Get("user") != null)
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
                ViewData["user"] = u;
                if (u.RoleID == 3)
                {
                    ViewData["appointmentID"] = appointmentID;
                    ViewData["patientInformationId"] = patientInformationId;
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
        public IActionResult OnPostCreateElectronicMedical()
        {
            Appointments appointments = _myMedDbContext.Appointments.FirstOrDefault(x=>x.AppointmentID == Input.AppointmentID);
            if (appointments != null)
            {
                appointments.Status = "Completed";
                _myMedDbContext.Entry<Appointments>(appointments).State = EntityState.Modified;
            }
            ElectronicMedicalRecords electronicMedicalRecords = new ElectronicMedicalRecords()
            {
                TestResults = Input.TestResults,
                TreatmentPlans = Input.TreatmentPlans,
                LastUpdated = DateTime.Now,
                IsDeleted = false,
                AppointmentID = Input.AppointmentID,
            };
            _myMedDbContext.ElectronicMedicalRecords.Add(electronicMedicalRecords);
            _myMedDbContext.SaveChanges();
            return RedirectToPage("/Doctor/ElectronicMedicalRecords", new { area = "User", patientInformationId = Input.PatientInformationId , appointmentID = Input.AppointmentID });
        }
    }
}
