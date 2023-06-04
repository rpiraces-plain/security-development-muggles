using ClickJacking.Classes;
using ClickJacking.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClickJacking.Controllers
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
        [ValidateAntiForgeryToken]
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