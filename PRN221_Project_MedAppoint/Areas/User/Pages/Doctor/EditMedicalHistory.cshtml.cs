using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PRN221_Project_MedAppoint.Model;
using System.Text;
using System.Text.Json;

namespace PRN221_Project_MedAppoint.Areas.User.Pages.Doctor
{
    public class EditMedicalHistoryModel : PageModel
    {
        private readonly MyMedDbContext _context;

        public EditMedicalHistoryModel(MyMedDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public ElectronicMedicalRecords ElectronicMedicalRecords { get; set; }
        public IActionResult OnGet(int eMR_ID, int patientInformationId)
        {
            if (HttpContext.Session.Get("user") != null)
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
                ViewData["user"] = u;
                if (u.RoleID == 3)
                {
                    ViewData["patientInformationId"] = patientInformationId;
                    ElectronicMedicalRecords = _context.ElectronicMedicalRecords.FirstOrDefault(x=>x.EMR_ID == eMR_ID);
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
        public IActionResult OnPostEditElectronicMedical(int eMR_ID,int patientInformationId)
        {
            ElectronicMedicalRecords electronicMedicalRecordsEdit = _context.ElectronicMedicalRecords.FirstOrDefault(x => x.EMR_ID == eMR_ID);
            electronicMedicalRecordsEdit.TestResults = ElectronicMedicalRecords.TestResults;
            electronicMedicalRecordsEdit.TreatmentPlans = ElectronicMedicalRecords.TreatmentPlans;
            electronicMedicalRecordsEdit.LastUpdated= DateTime.Now;
            _context.Attach(electronicMedicalRecordsEdit).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToPage("/Doctor/TrackingMedicalHistory", new { area = "User", patientInformationId });
        }
    }
}
