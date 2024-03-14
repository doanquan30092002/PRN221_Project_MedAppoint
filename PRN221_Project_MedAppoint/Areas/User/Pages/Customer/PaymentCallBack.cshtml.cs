using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_MedAppoint.Model;
using PRN221_Project_MedAppoint.Service;
using System.Text;
using System.Text.Json;

namespace PRN221_Project_MedAppoint.Areas.User.Pages.Customer
{
	public class PaymentCallBackModel : PageModel
	{
		private IMomoService _momoService;
		private readonly MyMedDbContext _context;

		public PaymentCallBackModel(IMomoService momoService, MyMedDbContext context)
		{
			_momoService = momoService;
			_context = context;
		}
		public MomoExecuteResponseModel response { get; set; }
		public async Task<IActionResult> OnGet()
		{
			response = await _momoService.PaymentExecuteAsync(HttpContext.Request.Query);

            byte[] userBytes = HttpContext.Session.Get("user");
			string serializedUser = Encoding.UTF8.GetString(userBytes);
			Users u = JsonSerializer.Deserialize<Users>(serializedUser);
            ViewData["user"] = u;

            Payments pay = new Payments
            {
                UserID = u.UserID,
                AppointmentID = int.Parse(response.AppointmentId),
                Amount = decimal.Parse(response.Amount),
                PaymentDate = DateTime.Now,
                Message = response.Message,
            };

            _context.Payments.Add(pay);
            await _context.SaveChangesAsync();

			return RedirectToPage("/Customer/Home"); 
		}
	}
}
