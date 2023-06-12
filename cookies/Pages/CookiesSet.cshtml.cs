using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;

namespace Cookies.Pages
{
    public class CookiesModel : PageModel
    {
        public string CookieValue { get; set; } = string.Empty;

        public void OnGet()
        {
            DateTime currentTime = DateTime.Now;
            string formattedTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss");

            var cookieTimeOpts = new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict
            };
            this.CookieValue = HttpContext.Request.Headers[HeaderNames.UserAgent] + " --- " + formattedTime;
            Response.Cookies.Append("BrowserAndTime", this.CookieValue, cookieTimeOpts);

            // Watch out this options! Insecure code!
            var cookieDotnetOpts = new CookieOptions
            {
                Secure = true,
                HttpOnly = false,
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None
            };
            Response.Cookies.Append("Dotnet2023Key", "1234Qwert");

        }
    }
}
