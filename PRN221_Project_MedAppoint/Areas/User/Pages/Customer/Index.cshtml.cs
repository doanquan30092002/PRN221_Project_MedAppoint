using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_MedAppoint.Model;

namespace PRN221_Project_MedAppoint.Areas.User.Pages.Customer
{
    public class IndexModel : PageModel
    {
        private readonly PRN221_Project_MedAppoint.Model.MyMedDbContext _context;

        public IndexModel(PRN221_Project_MedAppoint.Model.MyMedDbContext context)
        {
            _context = context;
        }

        public IList<Users> Users { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Users != null)
            {
                Users = await _context.Users
                .Include(u => u.Role).ToListAsync();
            }
        }
    }
}
