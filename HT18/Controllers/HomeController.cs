using _15.Models.Classes;
using _15.Models.Enums;
using HT18.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HT18.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static Exception? currentException;
        private static readonly Parking _parking = new()
        {
            Cars = new[] {new Car(Fuel.Disel, 2000, 3000, "Mercedes"), new Car(Fuel.Disel, 1000, 3000, "Toyota"), new Car(Fuel.Disel, 4000, 3000, "BMW") } 
        };

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (currentException != null)
            {
                ViewData["exception"] = currentException;
                currentException = null;
            }

            ViewData["Cars"] = _parking.Cars;
            return View();
        }

        public IActionResult AddCar(string identifier, Fuel fuel, int enginePower, int tankCapacity)
        {
            try
            {
                var car = new Car(fuel, enginePower, tankCapacity, identifier);
                _parking.AddCar(car);
            }
            catch (Exception ex)
            {
                currentException = ex;
            }

            return Redirect("Index");
        }

        public IActionResult RemoveCar(int index)
        {
            try
            {
                _parking.RemoveCar(_parking.Cars[index]);
            }
            catch (Exception ex)
            {
                currentException = ex;
            }

            return Redirect("Index");
        }

        public IActionResult FillCarTank(int index, int volume)
        {
            try
            {
                _parking.FillCarTank(_parking.Cars[index], volume);
            }
            catch (Exception ex)
            {
                currentException = ex;
            }

            return Redirect("Index");
        }

        public IActionResult SendCarForARide(int index)
        {
            try
            {
                _parking.SendCarForARide(_parking.Cars[index]);
            }
            catch (Exception ex)
            {
                currentException = ex;
            }

            return Redirect("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}