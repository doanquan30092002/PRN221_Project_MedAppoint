using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project_MedAppoint.Model;
using System.Text;
using System.Text.Json;

namespace PRN221_Project_MedAppoint.Areas.User.Pages
{
    public class LangModel : PageModel
    {
        public void OnGet()
        {
            byte[] userBytes = HttpContext.Session.Get("user");
            string serializedUser = Encoding.UTF8.GetString(userBytes);
            Users u = JsonSerializer.Deserialize<Users>(serializedUser);
            ViewData["user"] = u;
            ViewData["CheckHomeScreen"] = "true";
            
            string? culture = Request.Query["culture"];
            Console.WriteLine("new selected language: " + culture);
            if (culture != null)
            {
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            }

            string returnUrl = Request.Headers["Referer"].ToString() ?? "/";
            Response.Redirect(returnUrl);
        }
    }
}
