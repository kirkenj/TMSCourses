using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEB_EF.Models;
using WEB_EF.Models.Classes;
using WEB_EF.Models.DBContexts;

namespace WEB_EF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly AutoparkContext context = new AutoparkContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Names"] = context.Clients.Select(c => $"{c.Name} {c.Surname}, Owns cars with identifiers: {(c.Cars.Any() ? string.Join(", ", c.Cars.Select(c=> '"' + c.RegNumber + '"')): "Has no cars")}").ToList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}