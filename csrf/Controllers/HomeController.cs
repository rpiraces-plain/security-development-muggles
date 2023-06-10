using CSRF.Classes;
using CSRF.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CSRF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddToList()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken] // This validates the request
        public IActionResult AddToList(MovieListItem item)
        {
            MovieListHelper.addToList(item);
            return RedirectToAction("ShowList");
        }

        public IActionResult ShowList()
        {
            var movieList = MovieListHelper.getList();
            return View(movieList);
        }

        
        [HttpPost]
        // [ValidateAntiForgeryToken] // This validates the request
        public IActionResult ClearList()
        {
            MovieListHelper.clearList();
            return RedirectToAction("ShowList");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}