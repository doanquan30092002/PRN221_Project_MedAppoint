using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_MedAppoint.Model;
using System.Text;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PRN221_Project_MedAppoint.Areas.Admin.Pages
{
    public class ManageAppointmentModel : PageModel
	{
		private readonly MyMedDbContext _context;
		public ManageAppointmentModel(MyMedDbContext myMedDbContext)
		{
			_context = myMedDbContext;
		}
        public List<Appointments> Appointments { get; set; }
        // Filter
        [BindProperty]
        public string FilterCurrent { get; set; }

        // Paging
        public const int ITEMS_PER_PAGE = 4;

        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPage { get; set; }

        public int countPages { get; set; }
        // end Paging
        public IActionResult OnGet(string? FilterStatus)
        {
            if (HttpContext.Session.Get("user") != null)
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
                ViewData["user"] = _context.Users.FirstOrDefault(x=>x.UserID==u.UserID);
                if (u.RoleID == 1)
                {
                    // giữ giá trị 
                    FilterCurrent = FilterStatus;
                    IQueryable<Appointments> query = _context.Appointments.Include(x=>x.User).Include(x => x.Doctor).Include(x=>x.Specialist).Where(x=>x.IsDeleted==false);
                    if (!string.IsNullOrEmpty(FilterStatus) && !FilterStatus.Equals("AllStatus"))
                    {
                        query = query.Where(x => x.Status.Equals(FilterStatus));
                    }

                    // total page
                    int total = query.Count();
                    countPages = (int)Math.Ceiling((double)total / ITEMS_PER_PAGE);

                    if (currentPage < 1)
                        currentPage = 1;

                    if (currentPage > countPages)
                        currentPage = countPages;
                    //end
                    Appointments = query.Skip((currentPage - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToList();
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
        public IActionResult OnPostSelectedDoctorId()
        {
            return Redirect("/admin/login");
        }
    }
}

