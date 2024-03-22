using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project_MedAppoint.Model;
using System.Text;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PRN221_Project_MedAppoint.Areas.Admin.Pages
{
    public class ManageDoctorModel : PageModel
    {
        private readonly MyMedDbContext _context;
        public ManageDoctorModel(MyMedDbContext myMedDbContext)
        {
            _context = myMedDbContext;
        }
        public List<Users> Doctors { get; set; }
        // Paging
        public const int ITEMS_PER_PAGE = 4;

        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPage { get; set; }

        public int countPages { get; set; }
        // end Paging
        // giữ giá trị search
        public string CurrentFilter { get; set; }
        public IActionResult OnGet(string SearchString)
        {
            if (HttpContext.Session.Get("user") != null)
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
                ViewData["user"] = u;
                if (u.RoleID == 1)
                {
                    // giũ được giá trị search
                    CurrentFilter = SearchString;
                    IQueryable<Users> query = _context.Users.Where(x=>x.RoleID == 3);
                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        query = query.Where(x => x.Username.Contains(SearchString) || x.Phone.Contains(SearchString));
                    }
                    // total page
                    int totalDoctor = query.ToList().Count();
                    countPages = (int)Math.Ceiling((double)totalDoctor / ITEMS_PER_PAGE);

                    if (currentPage < 1)
                        currentPage = 1;

                    if (currentPage > countPages)
                        currentPage = countPages;
                    //
                    Doctors = query.OrderByDescending(x=>x.UserID).Skip((currentPage - 1) * ITEMS_PER_PAGE)
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
                return RedirectToPage("/Index", new { area = "Admin" });
            }
        }
    }
}

