using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_MedAppoint.Model;
using PRN221_Project_MedAppoint.Service;
using System.Text;
using System.Text.Json;

namespace PRN221_Project_MedAppoint.Areas.User.Pages.Customer
{
    public class HistoryAppointmentModel : PageModel
    {
        private readonly MyMedDbContext _context;

        public HistoryAppointmentModel(MyMedDbContext context)
        {
            _context = context;
        }

        // Paging
        public const int ITEMS_PER_PAGE = 8;

        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPage { get; set; }

        public int countPages { get; set; }
        // end Paging

        public List<Appointments> appointments { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.Get("user") != null)
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
                ViewData["user"] = u;
                if (u.RoleID == 2)
                {
                    GetListAndPaging(u);

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

        public async Task<IActionResult> OnGetCancelAppointment(int appointmentId)
        {
            byte[] userBytes = HttpContext.Session.Get("user");
            string serializedUser = Encoding.UTF8.GetString(userBytes);
            Users u = JsonSerializer.Deserialize<Users>(serializedUser);
            ViewData["user"] = u;

            // delete Appointment
            Appointments cancelApp = await _context.Appointments.FirstOrDefaultAsync(a => a.AppointmentID == appointmentId);
            cancelApp.Status = Status.Cancelled.ToString();
            cancelApp.IsDeleted = true;
            _context.SaveChanges();

            GetListAndPaging(u);

            // Redirect to the previous page
            return Page();
        }

        public void GetListAndPaging(Users u)
        {
            // get List
            appointments = _context.Appointments
                                        .Where(a => a.UserID == u.UserID)
                                        .Include(a => a.Doctor)
                                        .ToList();

            int totalAppointment = appointments.Count();
            countPages = (int)Math.Ceiling((double)totalAppointment / ITEMS_PER_PAGE);

            if (currentPage < 1)
                currentPage = 1;

            if (currentPage > countPages)
                currentPage = countPages;

            appointments = appointments.OrderBy(app => app.AppointmentID).OrderByDescending(app => app.Status)
                                        .Skip((currentPage - 1) * ITEMS_PER_PAGE)
                                        .Take(ITEMS_PER_PAGE)
                                        .ToList();
        }

    }
}
