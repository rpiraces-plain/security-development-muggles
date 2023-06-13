using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demos.Pages
{
    public class CSPModel : PageModel
    {
        private readonly ILogger<CSPModel> _logger;

        public CSPModel(ILogger<CSPModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // 1º Iteration: Collect endpoint and all blocked
            var cspHeader = "default-src 'self'; report-uri /collect";

            //var cspHeader = "default-src 'self'; " +
            //    "img-src 'self' https://dotnetconfspain.com/wp-content/webp-express/webp-images/uploads/2023/04/; " +
            //    "style-src 'self' https://cdn.jsdelivr.net 'unsafe-hashes' " + // Inline style hashes
            //    "'sha256-yckz1zrIL2HgQwm7x1ins99s5jndZE3XnmgOAkJvDOg=' " +
            //    "'sha256-cr42sjoWSSOFzzQSZXmLpq70OHNKYvOVmFo9ESrqcH0=' " +
            //    "'sha256-soFsOlcTWUItmB90RcCrKNGsLynvO+BSiqpMz8ETGTI='; " +
            //    "script-src 'self' https://cdn.jsdelivr.net 'sha256-aHqAVaN96ItafhbwPKIgMhwFrHlzzIoGL2jOqxNZxDM=' 'unsafe-eval'; " + // Allows the updateCount function
            //    "report-uri /collect;" + // Report endpoint in our server
            //    "connect-src wss://localhost:*"; // Web socket for debugging (local dev)

            HttpContext.Response.Headers.Add(
                "Content-Security-Policy",
                cspHeader);

            // Recommended now - CSP Level 3
            //HttpContext.Response.Headers.Add(
            //    "Content-Security-Policy-Report-Only",
            //    cspHeader);
        }
    }
}
