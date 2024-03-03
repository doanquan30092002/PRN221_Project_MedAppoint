using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_MedAppoint.Helpers;
using PRN221_Project_MedAppoint.Model;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using static PRN221_Project_MedAppoint.Areas.User.Pages.LoginModel;

namespace PRN221_Project_MedAppoint.Areas.User.Pages.Customer
{
    public class BookDoctorModel : PageModel
    {
        private readonly MyMedDbContext _context;

        public BookDoctorModel(MyMedDbContext context)
        {
            _context = context;
        }

        // Search input
        [BindProperty]
        public string searchInput { get; set; }

        // Filter input
        [BindProperty]
        [Range(0, double.MaxValue, ErrorMessage = "The {0} must be at least {1}")]
        public decimal minPrice { get; set; } = 0;

        [BindProperty]
        [Range(0, double.MaxValue, ErrorMessage = "The {0} must not exceed {1}")]
        public decimal maxPrice { get; set; } = 1500000;

        [BindProperty]
        public int SpecialistID { get; set; } 

        public List<SelectListItem> Specialists { get; set; }
        //public IList<Specialist> Specialists { get; set; }
        // end Filter input

        // Paging
        public const int ITEMS_PER_PAGE = 5;

        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPage { get; set; }

        public int countPages { get; set; }
        // end Paging

        public IList<UserWithSpecialtiesViewModel> Users { get; set; }

        

        public IActionResult OnGetAsync()
        {
            if (HttpContext.Session.Get("user") != null)
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
                ViewData["user"] = u;
                if (u.RoleID == 2)
                {
                    var specialistList = _context.Specialists.ToList();

                    // Chuyển đổi danh sách chuyên gia sang danh sách SelectListItem
                    Specialists = specialistList.Select(s => new SelectListItem
                    {
                        Value = s.SpecialistID.ToString(),
                        Text = s.SpecialtyName
                    }).ToList();

                    //Specialists = _context.Specialists.ToList();

                    Users = _context.Users
                                        .Include(u => u.Role)
                                        .Include(u => u.UsersToSpecialists)
                                            .ThenInclude(us => us.Specialist)
                                        .Where(u => u.RoleID == 3)
                                        .Select(u => new UserWithSpecialtiesViewModel
                                        {
                                            User = u,
                                            Specialties = u.UsersToSpecialists.Select(us => us.Specialist.SpecialtyName).ToList()
                                        })
                                        .ToList();

                    int totalDoctor = Users.Count();
                    countPages = (int)Math.Ceiling((double)totalDoctor / ITEMS_PER_PAGE);

                    if (currentPage < 1)
                        currentPage = 1;

                    if (currentPage > countPages)
                        currentPage = countPages;

                    Users = Users.OrderBy(doctor => doctor.User.UserID)
                                .Skip((currentPage - 1) * ITEMS_PER_PAGE)
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

        //Text search
        public IActionResult OnPostSearch()
        {
            if (HttpContext.Session.Get("user") != null)
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
                ViewData["user"] = u;
                if (u.RoleID == 2)
                {
                    var specialistList = _context.Specialists.ToList();

                    // Chuyển đổi danh sách chuyên gia sang danh sách SelectListItem
                    Specialists = specialistList.Select(s => new SelectListItem
                    {
                        Value = s.SpecialistID.ToString(),
                        Text = s.SpecialtyName
                    }).ToList();
                    Users = _context.Users
                                        .Include(u => u.Role)
                                        .Include(u => u.UsersToSpecialists)
                                            .ThenInclude(us => us.Specialist)
                                        .Where(u => u.RoleID == 3 &&
                                                    (u.Username.Contains(searchInput) ||
                                                    u.UsersToSpecialists.Any(us => us.Specialist.SpecialtyName.Contains(searchInput))))
                                        .Select(u => new UserWithSpecialtiesViewModel
                                        {
                                            User = u,
                                            Specialties = u.UsersToSpecialists.Select(us => us.Specialist.SpecialtyName).ToList()
                                        })
                                        .ToList();

                    int totalDoctor = Users.Count();
                    countPages = (int)Math.Ceiling((double)totalDoctor / ITEMS_PER_PAGE);

                    if (currentPage < 1)
                        currentPage = 1;

                    if (currentPage > countPages)
                        currentPage = countPages;

                    Users = Users.OrderBy(doctor => doctor.User.UserID)
                                .Skip((currentPage - 1) * ITEMS_PER_PAGE)
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

        //Filter search
        public IActionResult OnPostFilter()
        {
            if (HttpContext.Session.Get("user") != null)
            {
                byte[] userBytes = HttpContext.Session.Get("user");
                string serializedUser = Encoding.UTF8.GetString(userBytes);
                Users u = JsonSerializer.Deserialize<Users>(serializedUser);
                ViewData["user"] = u;
                if (u.RoleID == 2)
                {
                    var specialistList = _context.Specialists.ToList();

                    // Chuyển đổi danh sách chuyên gia sang danh sách SelectListItem
                    Specialists = specialistList.Select(s => new SelectListItem
                    {
                        Value = s.SpecialistID.ToString(),
                        Text = s.SpecialtyName
                    }).ToList();

                    // filter theo role, price, Specialist
                    Users = _context.Users
                        .Include(u => u.Role)
                        .Include(u => u.UsersToSpecialists)
                        .ThenInclude(us => us.Specialist)
                        .Where(u => u.RoleID == 3 &&
                                    (u.DoctorPrice >= minPrice && u.DoctorPrice <= maxPrice) &&
                                    (SpecialistID == 0 || u.UsersToSpecialists.Any(us => us.SpecialistID == SpecialistID)))
                        .Select(u => new UserWithSpecialtiesViewModel
                        {
                            User = u,
                            Specialties = u.UsersToSpecialists.Select(us => us.Specialist.SpecialtyName).ToList()
                        })
                        .ToList();

                    int totalDoctor = Users.Count();
                    countPages = (int)Math.Ceiling((double)totalDoctor / ITEMS_PER_PAGE);

                    if (currentPage < 1)
                        currentPage = 1;

                    if (currentPage > countPages)
                        currentPage = countPages;

                    Users = Users.OrderBy(doctor => doctor.User.UserID)
                        .Skip((currentPage - 1) * ITEMS_PER_PAGE)
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
