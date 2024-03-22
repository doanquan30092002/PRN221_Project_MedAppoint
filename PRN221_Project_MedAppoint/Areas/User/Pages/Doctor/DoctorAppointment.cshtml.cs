using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_MedAppoint.Model;
using PRN221_Project_MedAppoint.Pages;
using System.Text;
using System.Text.Json;

namespace PRN221_Project_MedAppoint.Areas.User.Pages.Doctor
{
    public class DoctorAppointmentModel : PageModel
    {
        private readonly MyMedDbContext _context;

        public DoctorAppointmentModel(MyMedDbContext context)
        {
            _context = context;
        }
        public List<Appointments> ListAppointments { get; set; }
        public string CurrentFilter { get; set; }
        // Paging
        public const int ITEMS_PER_PAGE = 4;

        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPage { get; set; }

        public int countPages { get; set; }
        // end Paging
        public IActionResult OnGet(string SearchString)
        {
            if (HttpContext.Session.Get("user") != null)
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
                ViewData["user"] = u;
                if (u.RoleID == 3)
                {
                    // giũ được giá trị search
                    CurrentFilter = SearchString;

                    IQueryable<Appointments> query = _context.Appointments.Include(x=>x.User)
                        .Include(x=>x.Specialist)
                        .Where(x=>x.DoctorID == u.UserID)
                        //.Where(x=>x.StartDate == DateTime.Now)
                        .Where(x=>x.Status.ToLower().Equals("confirmed"))
                        .Where(x=>x.IsDeleted==false);
                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        query = query.Where(x => x.User.Username.Contains(SearchString) || x.User.Phone.Contains(SearchString));
                    }
                    // total page
                    int totalDoctor = query.ToList().Count();
                    countPages = (int)Math.Ceiling((double)totalDoctor / ITEMS_PER_PAGE);

                    if (currentPage < 1)
                        currentPage = 1;

                    if (currentPage > countPages)
                        currentPage = countPages;
                    //
                    ListAppointments = query.Skip((currentPage - 1) * ITEMS_PER_PAGE)
                                .Take(ITEMS_PER_PAGE)
                                .ToList();
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
